from bs4 import BeautifulSoup
import requests

def get_data():
    """Scrapes crypto name, symbol and price from Coinlib"""
    data = []

    source = requests.get("https://coinlib.io/")

    soup = BeautifulSoup(source.text, 'lxml')

    table = soup.find('table')

    for row in table.find_all('tr', limit=51):
        try:
            name = row.find('a').text.strip()
            symbol = row.find('span', class_='tbl-coin-abbrev').text.strip()[1:-1]
            price_usdt = row.select_one('span[class*="tbl-price avgprice"]').text.strip()
            change_24 = row.select_one('span[class*="tbl-price pr-change delta"]').text.strip()

        except AttributeError:
            continue

        data.append({"name": name, "symbol": symbol, "price": price_usdt, "change": change_24})
   
    return data
