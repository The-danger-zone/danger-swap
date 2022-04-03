import unittest

from scrapers.scrape import get_data

class TestScraper(unittest.TestCase):

    def setUp(self):
        self.data = get_data()

    def test_data_not_empty(self):
        self.assertTrue(self.data)

    def test_dict_length(self):
        self.assertEqual(10, len(self.data))

    def test_price_length(self):
        self.assertEqual(10, len(self.data.values()))


if __name__ == '__main__':
    unittest.main()