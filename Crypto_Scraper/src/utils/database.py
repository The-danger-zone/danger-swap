import sqlite3
from sqlite3 import Error

def create_connection(db_file):
    """ create a database connection to the SQLite database
        specified by the db_file
    :param db_file: database file
    :return: Connection object or None
    """
    conn = None
    try:
        conn = sqlite3.connect(db_file)
    except Error as e:
        print(e)

    return conn

def update_rate(conn, coin):
    """
    Update currency rate
    """
    sql = '''UPDATE Rate
              SET Rate = ?
              WHERE Currency = ?'''
    cur = conn.cursor()
    cur.execute(sql, coin)
    conn.commit()

def insert_rate(conn, coin):
    """
    Insert currency rate
    """
    sql = '''INSERT INTO Rate
            (Currency, Rate)
            VALUES(?)'''
    cur = conn.cursor()
    cur.execute(sql, coin)
    conn.commit()