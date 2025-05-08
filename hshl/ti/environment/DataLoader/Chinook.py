import os
import Database

def import_data():
    f = open("databases/chinook.sql", "r")
    sql = f.read()
    f.close()

    connection = Database.get_connection()
    with connection.cursor() as cur:
        cur.execute(sql)

