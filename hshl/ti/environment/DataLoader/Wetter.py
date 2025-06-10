import Database
import requests
from datetime import datetime, timedelta
import zipfile
import io

stations = {}
measurements = {}

def clear():
    connection = Database.get_connection()
    with connection.cursor() as cur:
        cur.execute("drop schema if exists wetter CASCADE;")
        cur.execute("create schema wetter;")
        cur.execute("""create table wetter.stationen (
                    stations_id serial primary key,
                    name varchar(200) not null,
                    hoehe double precision not null,
                    geo_breite double PRECISION not null,
                    geo_hoehe double PRECISION not null,
                    location geography(POINT),
                    bundesland varchar(200) not null);""")
        cur.execute("""create table wetter.messungen (
                    stations_id int not null,
                    zeitpunkt timestamp not null,
                    temperatur_2m double precision,
                    rel_feuchte double precision,
                    windgeschwindigkeit double precision,
                    windrichtung double precision,
                    sonnenscheindauer double precision,
                    luftdruck double precision);""")

def load_stationen():
    load_stationen_from("air_temperature/recent/TU_Stundenwerte_Beschreibung_Stationen.txt", True)
    load_stationen_from("wind/recent/FF_Stundenwerte_Beschreibung_Stationen.txt")
    load_stationen_from("sun/recent/SD_Stundenwerte_Beschreibung_Stationen.txt")
    load_stationen_from("pressure/recent/P0_Stundenwerte_Beschreibung_Stationen.txt")
    global measurements

    connection = Database.get_connection()
    with connection.cursor() as cur:
        for id in stations.keys():
            if stations[id]['count'] == 3:
                measurements[id] = {}
                sql = f"""insert into wetter.stationen (stations_id, name, hoehe, geo_breite, geo_hoehe, bundesland, location) values
                    ({id}, '{stations[id]['name']}', {stations[id]['hoehe']}, {stations[id]['geo_breite']}, 
                    {stations[id]['geo_hoehe']}, '{stations[id]['bundesland']}', 
                    ST_GeogFromtext('POINT({stations[id]['geo_hoehe']} {stations[id]['geo_breite']})'))"""
                cur.execute(sql)

def load_stationen_from(url, only_if_exists=False):
    url = f"https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/hourly/{url}"
    response = requests.get(url)
    lines = response.text.split("\r\n")
    last = datetime.now().date() - timedelta(days=2)

    global stations
    for i in range(2, len(lines) - 1):
        try:
            id = get_int_from_part(lines[i], 0,5)
            from_date = get_date_from_part(lines[i], 6, 8)
            to_date = get_date_from_part(lines[i], 15,8)
            hoehe = get_float_from_part(lines[i], 24, 14)
            geo_breite = get_float_from_part(lines[i], 41, 11)
            geo_hoehe = get_float_from_part(lines[i], 51, 9)
            name = get_part(lines[i], 61, 40)
            bundesland = get_part(lines[i], 102,40)

            if to_date >= last:
                count = 0 if id not in stations.keys() else stations[id]['count'] + 1
                stations[id] = {'name': name, 'from': from_date, 'to': to_date, 'hoehe': hoehe, 'geo_breite': geo_breite,
                    'geo_hoehe': geo_hoehe, 'bundesland': bundesland, 'count': count}
        except:
            print(f"Could not parse line {i}: {lines[i]}")

def get_part(str, start, len):
    result = str[start:(start+len)].rstrip()
    return result

def get_int_from_part(str, start, len):
    part = get_part(str, start, len)
    try:
        return int(part)
    except:
        print(f"Could not parse '{part}' to int")
        return 0

def get_float_from_part(str, start, len):
    part = get_part(str, start, len)
    try:
        return float(part)
    except:
        print(f"Could not parse '{part}' to float")
        return 0

def get_date_from_part(str, start, len):
    part = get_part(str, start, len)
    try:
        return datetime.strptime(part, "%Y%m%d").date()
    except:
        print(f"Could not parse '{part}' to float")
        return datetime().date()

def load_messung(id):
    global measurements
    load_temperatures(id)
    load_wind(id)
    load_sunshine(id)
    load_preasure(id)

def load_messungen():
    for id in [id for id, data in stations.items() if data.get('count', 0) >= 3]:
        load_messung(id)
    save_measurements()

def update_measurements(dict):
    global measurements
    last_year = datetime.now() - timedelta(days=366)
    if dict['zeit'] > last_year:
        if dict['zeit'] in measurements[dict['id']].keys():
            measurements[dict['id']][dict['zeit']].update(dict)
        else:
            measurements[dict['id']][dict['zeit']] = dict

def load_temperatures(id):
    print(f"Loading temperatures for {id}")
    last = datetime.now() - timedelta(days=366)
    url = f"https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/hourly/air_temperature/recent/stundenwerte_TU_{id:05d}_akt.zip"
    content = load_produkt_from_zip(url)
    if content is not None:
        lines = content.split("\r\n")
        for i in range(1, len(lines) - 1):
            items = lines[i].split(";")
            data = {"id": int(items[0]), "zeit": datetime.strptime(items[1], "%Y%m%d%H"), "temperatur_2m": float(items[3]), "rel_feuchte": float(items[4])}
            update_measurements(data)

def load_wind(id):
    print(f"Loading wind for {id}")
    global measurements
    last = datetime.now() - timedelta(days=366)
    url = f"https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/hourly/air_temperature/recent/stundenwerte_TU_{id:05d}_akt.zip"
    content = load_produkt_from_zip(url)
    if content is not None:
        lines = content.split("\r\n")
        for i in range(1, len(lines) - 1):
            items = lines[i].split(";")
            data = {"id": int(items[0]), "zeit": datetime.strptime(items[1], "%Y%m%d%H"), "windgeschwindigkeit": float(items[3]), "windrichtung": float(items[4])}
            update_measurements(data)

def load_sunshine(id):
    print(f"Loading sunshine for {id}")
    global measurements
    last = datetime.now() - timedelta(days=366)
    url = f"https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/hourly/sun/recent/stundenwerte_SD_{id:05d}_akt.zip"
    content = load_produkt_from_zip(url)
    if content is not None:
        lines = content.split("\r\n")
        for i in range(1, len(lines) - 1):
            items = lines[i].split(";")
            data = {"id": int(items[0]), "zeit": datetime.strptime(items[1], "%Y%m%d%H"), "sonnenscheindauer": float(items[3])}
            update_measurements(data)

def load_preasure(id):
    print(f"Loading presure for {id}")
    global measurements
    last = datetime.now() - timedelta(days=366)
    url = f"https://opendata.dwd.de/climate_environment/CDC/observations_germany/climate/hourly/pressure/recent/stundenwerte_P0_{id:05d}_akt.zip"
    content = load_produkt_from_zip(url)
    if content is not None:
        lines = content.split("\r\n")
        for i in range(1, len(lines) - 1):
            items = lines[i].split(";")
            data = {"id": int(items[0]), "zeit": datetime.strptime(items[1], "%Y%m%d%H"), "luftdruck": float(items[3])}
            update_measurements(data)

def load_produkt_from_zip(zip_url):
    try:
        response = requests.get(zip_url)
        with zipfile.ZipFile(io.BytesIO(response.content)) as zip_file:
            for filename in zip_file.namelist():
                if filename.startswith("produkt_"):
                    with zip_file.open(filename) as file:
                        file_content = file.read()
                        return file_content.decode()
    except:
        return None
    return None

def save_measurements():
    print("Saving measurements...")
    connection = Database.get_connection()
    sql = []
    for id in measurements.keys():
        for zeit in measurements[id].keys():
            temperatur_2m = measurements[id][zeit]['temperatur_2m'] if 'temperatur_2m' in measurements[id][zeit] else 'NULL'
            rel_feuchte = measurements[id][zeit]['rel_feuchte'] if 'rel_feuchte' in measurements[id][zeit] else 'NULL'
            windgeschwindigkeit = measurements[id][zeit]['windgeschwindigkeit'] if 'windgeschwindigkeit' in measurements[id][zeit] else 'NULL'
            windrichtung = measurements[id][zeit]['windrichtung'] if 'windrichtung' in measurements[id][zeit] else 'NULL'
            sonnenscheindauer = measurements[id][zeit]['sonnenscheindauer'] if 'sonnenscheindauer' in measurements[id][zeit] else 'NULL'
            luftdruck = measurements[id][zeit]['luftdruck'] if 'luftdruck' in measurements[id][zeit] else 'NULL'
            sql.append(f"({id}, '{zeit.strftime('%Y-%m-%d %H:%M:%S')}', {temperatur_2m}, {rel_feuchte}, "
                       f"{windgeschwindigkeit}, {windrichtung}, {sonnenscheindauer}, {luftdruck})")

    with connection.cursor() as cur:
        statement = """insert into wetter.messungen (stations_id, zeitpunkt, temperatur_2m, rel_feuchte, 
        windgeschwindigkeit, windrichtung, sonnenscheindauer, luftdruck) values """ + ",".join(sql) + ";"
        cur.execute(statement)

