import requests
from bs4 import BeautifulSoup

def get_data():
    url = 'https://www.x-rates.com/table/?from=USD&amount=1'
    r = requests.get(url)

    currency_dict = {}

    if r.status_code == 200:
        soup = BeautifulSoup(r.text, 'html.parser')
        rate_table = soup.find('table', {'class': 'ratesTable'})

        for currencies in rate_table.find_all('tbody', limit=50):
            rows = currencies.find_all('tr')
            for row in rows:
                currencyName = row.find('td').text
                for exchange in row.find_all('td', {'class': 'rtRates'})[1:]:
                    currency_dict[currencyName] = exchange.text
    else:
        print("Connection is lost")

    return currency_dict