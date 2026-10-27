-- Musterloesungen zu den SQL-Aufgaben der Uebung (Kapitel 13)
-- Erst ansehen, wenn Sie die Aufgaben selbst geloest haben.
-- Ausfuehren z. B. mit: docker compose exec -T timescaledb psql -U postgres -d energie < loesung/abfragen.sql

-- Eigene Abfragen, Basis 1: Messwerte je Geraet
SELECT geraet, count(*) AS anzahl
FROM messwerte
GROUP BY geraet;

-- Basis 2: erster und letzter Zeitstempel je Geraet
SELECT geraet, min(zeit) AS erster, max(zeit) AS letzter
FROM messwerte
GROUP BY geraet;

-- Basis 3: die zehn Stunden mit dem hoechsten Verbrauch des Hauses
SELECT zeit, kw
FROM verbrauch_stunde
WHERE geraet = 'wmz-haupt'
ORDER BY kw DESC
LIMIT 10;

-- Vertiefung: Tagesverbrauch im Januar 2026, Tage in Ortszeit
SELECT time_bucket('1 day', zeit, 'Europe/Berlin') AS tag,
       round(sum(kw)::numeric, 1) AS verbrauch_kwh
FROM verbrauch_stunde
WHERE geraet = 'wmz-haupt'
  AND zeit >= '2026-01-01 00:00+01' AND zeit < '2026-02-01 00:00+01'
GROUP BY tag
ORDER BY tag;

-- Herausforderung Luecken: erwartete und vorhandene Stunden
SELECT count(*) AS vorhanden,
       EXTRACT(EPOCH FROM max(zeit) - min(zeit)) / 3600 + 1 AS erwartet
FROM messwerte
WHERE geraet = 'wmz-haupt';

-- Herausforderung Luecken: Stunden ohne Messwert
-- Fuer aufgefuellte Stunden liefert count() NULL. MATERIALIZED sorgt dafuer, dass die
-- Bedingung erst nach dem Auffuellen geprueft wird.
WITH s AS MATERIALIZED (
  SELECT time_bucket_gapfill('1 hour', zeit) AS stunde,
         count(energie_kwh) AS anzahl
  FROM messwerte
  WHERE geraet = 'wmz-haupt'
    AND zeit >= '2025-10-01 00:00+00' AND zeit < '2026-04-01 00:00+00'
  GROUP BY stunde
)
SELECT stunde
FROM s
WHERE anzahl IS NULL
ORDER BY stunde;

-- Messlatte: MAE je Verfahren in der Testwoche (vorher Abfrage 4 aus abfragen/beispiele.sql ausfuehren)
SELECT p.verfahren,
       count(*)                                   AS stunden,
       round(avg(abs(p.wert - v.kw))::numeric, 2) AS mae_kw
FROM prognosen p
JOIN verbrauch_stunde v ON v.zeit = p.zeit
WHERE v.geraet = 'wmz-haupt'
GROUP BY p.verfahren
ORDER BY mae_kw;

-- Herausforderung: Vortag des Stichtags siebenmal wiederholen
INSERT INTO prognosen (zeit, verfahren, wert)
SELECT zeit + INTERVAL '1 day' * (n + 1), 'saisonal naiv Tag (SQL)', kw
FROM verbrauch_stunde, generate_series(0, 6) AS n
WHERE geraet = 'wmz-haupt'
  AND zeit >= TIMESTAMPTZ '2026-01-08 00:00+00' - INTERVAL '1 day'
  AND zeit <  TIMESTAMPTZ '2026-01-08 00:00+00'
ON CONFLICT (verfahren, zeit) DO UPDATE SET wert = EXCLUDED.wert;
