-- Schema fuer das Beispiel "Waermeverbrauch eines Mehrfamilienhauses".
-- Wird beim ersten Start des Containers automatisch ausgefuehrt.

CREATE EXTENSION IF NOT EXISTS timescaledb;

-- Rohwerte der Waermezaehler, so wie sie per LoRaWAN ankommen
CREATE TABLE messwerte (
  zeit        TIMESTAMPTZ NOT NULL,
  geraet      TEXT        NOT NULL,
  energie_kwh DOUBLE PRECISION,   -- Zaehlerstand
  vorlauf_c   DOUBLE PRECISION,
  ruecklauf_c DOUBLE PRECISION,
  PRIMARY KEY (geraet, zeit)
);
SELECT create_hypertable('messwerte', by_range('zeit'));

-- Aussentemperatur (in der Realitaet vom Wetterdienst)
CREATE TABLE wetter (
  zeit          TIMESTAMPTZ PRIMARY KEY,
  temperatur_c  DOUBLE PRECISION
);
SELECT create_hypertable('wetter', by_range('zeit'));

-- Prognosen, die z. B. prognose.py zurueckschreibt
CREATE TABLE prognosen (
  zeit        TIMESTAMPTZ NOT NULL,
  verfahren   TEXT        NOT NULL,
  wert        DOUBLE PRECISION,
  unten       DOUBLE PRECISION,
  oben        DOUBLE PRECISION,
  erstellt_am TIMESTAMPTZ NOT NULL DEFAULT now(),
  PRIMARY KEY (verfahren, zeit)
);
SELECT create_hypertable('prognosen', by_range('zeit'));

-- Verdichtung: letzter Zaehlerstand und mittlere Temperaturen je Stunde
CREATE MATERIALIZED VIEW zaehler_stunde
WITH (timescaledb.continuous) AS
SELECT time_bucket('1 hour', zeit) AS zeit,
       geraet,
       last(energie_kwh, zeit) AS zaehlerstand,
       avg(vorlauf_c)          AS vorlauf_c,
       avg(ruecklauf_c)        AS ruecklauf_c
FROM messwerte
GROUP BY 1, 2
WITH NO DATA;

SELECT add_continuous_aggregate_policy('zaehler_stunde',
  start_offset      => NULL,
  end_offset        => INTERVAL '1 hour',
  schedule_interval => INTERVAL '5 minutes');

-- Verbrauch je Stunde: Differenz zweier Zaehlerstaende, geteilt durch die
-- Stunden dazwischen (bei Luecken), Zeitstempel = Beginn des Intervalls
CREATE VIEW verbrauch_stunde AS
SELECT zeit, geraet, kw AS verbrauch_kwh, kw, vorlauf_c, ruecklauf_c
FROM (
  SELECT zeit, geraet, vorlauf_c, ruecklauf_c,
         (LEAD(zaehlerstand) OVER w - zaehlerstand)
           / (EXTRACT(EPOCH FROM LEAD(zeit) OVER w - zeit) / 3600) AS kw
  FROM zaehler_stunde
  WINDOW w AS (PARTITION BY geraet ORDER BY zeit)
) AS v
WHERE kw IS NOT NULL;

-- Rohdaten nach 30 Tagen komprimieren, nach fuenf Jahren loeschen
ALTER TABLE messwerte SET (timescaledb.compress, timescaledb.compress_segmentby = 'geraet');
SELECT add_compression_policy('messwerte', INTERVAL '30 days');
SELECT add_retention_policy('messwerte', INTERVAL '5 years');
