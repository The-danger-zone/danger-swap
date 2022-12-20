import unittest, json
from parsers.json_parser import parse
from scrapers.scrape_coingecko import get_data

class TestJsonParser(unittest.TestCase):

    def test_parser_valid_input(self):
        with self.assertRaises(Exception):
            try:
                coins = get_data()
                parse(coins)
            except:
                pass
            else:
                raise Exception
        
      
if __name__ == '__main__':
    unittest.main()