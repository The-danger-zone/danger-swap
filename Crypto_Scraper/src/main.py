from asyncore import write
import time

import scrapers.scrape_coingecko
import scrapers.scrape_coinlib
from filters.crypto_filter import filter_coins
from parsers.json_parser import parse
from utils.file_writer import write_to_file


if __name__ == "__main__":

    required_coins = ['BTC', 'ETH', 'ADA', 'DOT']

    while True:
        try:
            coins = scrapers.scrape_coingecko.get_data()

            if not coins:
                coins = scrapers.scrape_coinlib.get_data() 

            filtered_coins = filter_coins(coins, required_coins)

            write_to_file(parse(filtered_coins))

            time.sleep(10)
        except:
            raise TypeError
