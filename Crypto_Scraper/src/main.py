import time

import scrapers.scrape_coingecko
import scrapers.scrape_coinlib
from filters.crypto_filter import filter_coins
from utils import database


if __name__ == "__main__":

    required_coins = ['BTC', 'ETH', 'ADA', 'DOT']

    while True:
        try:
            coins = scrapers.scrape_coingecko.get_data()

            if not coins:
                coins = scrapers.scrape_coinlib.get_data() 

            filtered_coins = filter_coins(coins, required_coins)

            conn = database.create_connection()
            with conn:
                for coin in coins:
                    database.update_rate(conn, (coin["price"], coin["symbol"]))

            time.sleep(10)
        except:
            raise TypeError
