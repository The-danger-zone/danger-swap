from bs4 import BeautifulSoup
from selenium import webdriver
from webdriver_manager.chrome import ChromeDriverManager

def get_data():
    """Scrapes crypto name, symbol and price from CoinGecko"""

    data = []

    driver = webdriver.Chrome(ChromeDriverManager().install())
    driver.get("https://www.coingecko.com/")

    html = driver.page_source

    soup = BeautifulSoup(html, 'lxml')

    table = soup.find('table')

    for row in table.find_all('tr', limit=50):
        try:
            name = row.find('span', class_='lg:tw-flex font-bold tw-items-center tw-justify-between').text.strip()
            symbol = row.find('span', class_='d-lg-inline font-normal text-3xs tw-ml-0 md:tw-ml-2 md:tw-self-center tw-text-gray-500 dark:tw-text-white dark:tw-text-opacity-60').text.strip()
            price_usdt = row.find('span', class_='no-wrap').text.strip()
            change_24 = row.find('span', {"data-24h" : "true"}).text.strip()

        except AttributeError:
            continue
        
        data.append({"name": name, "symbol": symbol, "price": float(price_usdt[1:].replace(",","")), "change": change_24})

    return data
