import time

import scrapers.scrape_coingecko
import scrapers.scrape_coinlib
from filters.crypto_filter import filter_coins
from parsers import json_parser


if __name__ == "__main__":

    required_coins = ['BTC', 'ETH', 'ADA', 'DOT']

    while True:
            coins = scrapers.scrape_coingecko.get_data()

            if not coins:
                coins = scrapers.scrape_coinlib.get_data() 

            filtered_coins = filter_coins(coins, required_coins)
            json_coins = json_parser.parse(filtered_coins)

            time.sleep(10)
