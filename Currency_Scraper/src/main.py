import time

import scrapers.currency_scraper
from filters.currency_filter import filter_currency
from utilities.file_writer import write_to_file
import json

if __name__ == "__main__":

    key_currency_values = ['Euro', 'British Pound', 'Chinese Yuan Renminbi']

    while True:
        try:
            currencies = scrapers.currency_scraper.get_data()
            filtered_currencies = filter_currency(currencies, key_currency_values)
            parsed_currencies = json.dumps(filtered_currencies, indent=4)
            write_to_file(parsed_currencies)
            time.sleep(60)
        except:
            raise TypeError
