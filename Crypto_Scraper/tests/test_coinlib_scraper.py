import unittest
from scrapers.scrape_coinlib import get_data

class TestCoinLibScraper(unittest.TestCase):

    def setUp(self):
        self.coins = get_data()

    def test_data_not_empty(self):
        self.assertTrue(self.coins)

    def test_coin_data_amount(self):
        self.assertEqual(50, len(self.coins))

    def test_numof_data_values(self):
        self.assertEqual(4, len(self.coins[0]))

if __name__ == '__main__':
    unittest.main()
