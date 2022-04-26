from scrapers import currency_scraper
from filters import filter
from utilities import file_writer
import json
import time

if __name__ == "__main__":

    key_currency_values = ['Euro', 'British Pound', 'Chinese Yuan Renminbi']

    while True:
        try:
            currencies = currency_scraper.get_data()
            filtered_currencies = filter.filter_currency(currencies, key_currency_values)
            parsed_currencies = json.dumps(filtered_currencies, indent=4)
            file_writer.write_to_file(parsed_currencies)
            time.sleep(60)
        except:
            raise TypeError

