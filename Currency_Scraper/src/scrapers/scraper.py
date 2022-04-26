import requests
from bs4 import BeautifulSoup


def get_data():
    url = 'https://www.x-rates.com/table/?from=USD&amount=1'
    request = requests.get(url)

    currency_dict = {}

    if request.status_code == 200:
        soup = BeautifulSoup(request.text, 'html.parser')

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

def currency_filter(dictionary):
    filtered_dict = {}
    keyCurrencyValues = ['Euro', 'British Pound', 'Chinese Yuan Renminbi']

    for key, values in dictionary.items():
        if key in keyCurrencyValues:
            filtered_dict[key] = values

    return filtered_dict
