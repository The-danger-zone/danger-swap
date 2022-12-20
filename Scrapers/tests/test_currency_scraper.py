import unittest

from scrapers.scrape_fiat import get_data


class MyTestCase(unittest.TestCase):
    def setUp(self):
        self.data = get_data()

    def test_data_not_empty(self):
        self.assertTrue(self.data)

    def test_dict_length(self):
        self.assertEqual(3, len(self.data))


if __name__ == '__main__':
    unittest.main()