import unittest
from scrapers.scrape_coingecko import get_data

class TestCoinGeckoScraper(unittest.TestCase):

    def setUp(self):
        self.coins = get_data()

    def test_data_not_empty(self):
        self.assertTrue(self.coins)

    def test_coin_data_amount(self):
        self.assertEqual(49, len(self.coins))

    def test_numof_data_values(self):
        self.assertEqual(4, len(self.coins[0]))

if __name__ == '__main__':
    unittest.main()
