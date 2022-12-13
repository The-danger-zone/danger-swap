import requests
from bs4 import BeautifulSoup

def get_data():
    url = 'https://www.x-rates.com/table/?from=USD&amount=1'
    request = requests.get(url)

    data = []
    if request.status_code == 200:
        soup = BeautifulSoup(request.text, 'html.parser')

        rate_table = soup.find('table', {'class': 'ratesTable'})
        for currencies in rate_table.find_all('tbody', limit=50):
            rows = currencies.find_all('tr')
            for row in rows:
                currency_name = row.find('td').text
                for exchange in row.find_all('td', {'class': 'rtRates'})[1:]:
                    price = float(exchange.text)
                    formatted_price = float("{:.2f}".format(price))
                    if currency_name == "Euro":
                        data.append({"name": currency_name, "symbol": "EUR", "price": formatted_price})
                    elif currency_name  == "British Pound":
                        data.append({"name": currency_name, "symbol": "GBP", "price": formatted_price})
                    elif currency_name == "Chinese Yuan Renminbi":
                        data.append({"name": currency_name, "symbol": "CNY", "price": formatted_price})
    else:
        print("Connection is lost")

    return data