import unittest

from filters.currency_filter import filter_currency


class TestFilter(unittest.TestCase):
    def test_currency_filter_with_no_currencies(self):
        currencies = {}
        key_currency_values = ['Euro', 'British Pound', 'Chinese Yuan Renminbi']
        self.assertRaises(ValueError, filter_currency, *[currencies, key_currency_values])

    def test_currency_filter_with_no_key_values(self):
        currencies = {
            "Euro": "1.1",
            "British Pound": "1.5",
            "Chinese Yuan Renminbi": "20",
            "Indian Rupee": "1.6"
        }
        key_currency_values = []

        self.assertRaises(ValueError, filter_currency, *[currencies, key_currency_values])

    def test_currency_filter_with_key_values(self):
        currencies = {
            "Euro": "1.1",
            "British Pound": "1.5",
            "Chinese Yuan Renminbi": "20",
            "Indian Rupee": "1.6"
        }
        key_currency_values = ['Euro', 'British Pound', 'Chinese Yuan Renminbi']

        self.assertRaises(ValueError, filter_currency, *[currencies, key_currency_values])

if __name__ == '__main__':
    unittest.main()
