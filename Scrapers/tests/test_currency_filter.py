import unittest

from filters.filter import filter_currencies


class TestFilter(unittest.TestCase):
    def test_currency_filter_with_no_currencies(self):
        currencies = []
        key_currency_values = ['EUR', 'GBP', 'CNY']
        self.assertRaises(ValueError, filter_currencies, *[currencies, key_currency_values])

    def test_currency_filter_with_no_key_values(self):
        currencies = [
        {'name': 'Euro', 'symbol': 'EUR', 'price': '1.2'},
        {'name': 'British Pound', 'symbol': 'GBP', 'price': '1.3'},
        {'name': 'Chinese Yuan Renminbi', 'symbol': 'CNY', 'price': '1.4'}
        ]
        key_currency_values = []

        self.assertRaises(ValueError, filter_currencies, *[currencies, key_currency_values])

    def test_currency_filter_with_key_values(self):
        currencies = [
        {'name': 'Euro', 'symbol': 'EUR', 'price': '1.2'},
        {'name': 'British Pound', 'symbol': 'GBP', 'price': '1.3'},
        {'name': 'Chinese Yuan Renminbi', 'symbol': 'CNY', 'price': '1.4'}
        ]
        key_currency_values = ['EUR']

        filtered_currencies = filter_currencies(currencies, key_currency_values)
        assert len(filtered_currencies) == 1

    def test_currency_with_no_values(self):
        currencies = []
        key_currency_values = []
        self.assertRaises(ValueError, filter_currencies, *[currencies, key_currency_values])

    def test_no_required_coins(self):
        currencies = [
            {'name': 'Euro', 'symbol': 'EUR', 'price': '1.2'},
            {'name': 'British Pound', 'symbol': 'GBP', 'price': '1.3'},
            {'name': 'Chinese Yuan Renminbi', 'symbol': 'CNY', 'price': '1.4'}
        ]
        key_currency_values = ['AUD']
        filtered_currencies = filter_currencies(currencies, key_currency_values)
        assert len(filtered_currencies) == 0


if __name__ == '__main__':
    unittest.main()