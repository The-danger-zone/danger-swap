import unittest
from scrapers.scrape_binance import get_data
class TestBinanceScraper(unittest.TestCase):

    def setUp(self):
        self.required_coins = [{"name": "Bitcoin", "symbol": "BTC", "payload": "BTCUSDT"},
                            {"name": "Ethereum", "symbol": "ETH", "payload": "ETHUSDT"}]
        self.coins = get_data(self.required_coins)

    def test_data_not_empty(self):
        self.assertTrue(self.coins)

    def test_coin_data_amount(self):
        print(len(self.coins))
        self.assertEqual(2, len(self.coins))

    def test_numof_data_values(self):
        self.assertEqual(4, len(self.coins[0]))

if __name__ == '__main__':
    unittest.main()
