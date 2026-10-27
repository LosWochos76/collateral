-- Beispielabfragen aus der Vorlesung (Kapitel 12)
-- Ausfuehren z. B. mit: docker compose exec -T timescaledb psql -U postgres -d energie < abfragen/beispiele.sql

-- 1. Verbrauch je Stunde aus den Zaehlerstaenden (Fensterfunktion LAG)
SELECT zeit, geraet, energie_kwh,
       energie_kwh - LAG(energie_kwh) OVER (PARTITION BY geraet ORDER BY zeit) AS verbrauch_kwh
FROM messwerte
WHERE geraet = 'wmz-haupt'
ORDER BY zeit
LIMIT 5;

-- 2. Tagesverbrauch in Ortszeit mit time_bucket
SELECT time_bucket('1 day', zeit, 'Europe/Berlin') AS tag,
       round(sum(kw)::numeric, 1)          AS verbrauch_kwh,
       round(max(ruecklauf_c)::numeric, 1) AS ruecklauf_max_c
FROM verbrauch_stunde
WHERE geraet = 'wmz-haupt' AND zeit >= '2026-01-12' AND zeit < '2026-01-16'
GROUP BY tag
ORDER BY tag;

-- 3. Luecken sichtbar machen und fuellen
SELECT time_bucket_gapfill('1 hour', zeit) AS stunde,
       last(energie_kwh, zeit)              AS stand,
       locf(last(energie_kwh, zeit))        AS stand_locf,
       interpolate(last(energie_kwh, zeit)) AS stand_interpoliert
FROM messwerte
WHERE geraet = 'wmz-haupt'
  AND zeit >= '2025-11-18 02:00+00' AND zeit < '2025-11-18 09:00+00'
GROUP BY stunde
ORDER BY stunde;

-- 4. Saisonal naive Prognose: die naechste Woche wie die letzte Woche
INSERT INTO prognosen (zeit, verfahren, wert)
SELECT zeit + INTERVAL '7 days', 'saisonal naiv (SQL)', kw
FROM verbrauch_stunde
WHERE geraet = 'wmz-haupt'
  AND zeit >= TIMESTAMPTZ '2026-01-08 00:00+00' - INTERVAL '7 days'
  AND zeit <  TIMESTAMPTZ '2026-01-08 00:00+00'
ON CONFLICT (verfahren, zeit) DO UPDATE SET wert = EXCLUDED.wert;

-- 5. Wie ist die Hypertable aufgeteilt?
SELECT chunk_name, range_start, range_end, is_compressed
FROM timescaledb_information.chunks
WHERE hypertable_name = 'messwerte'
ORDER BY range_start
LIMIT 5;
