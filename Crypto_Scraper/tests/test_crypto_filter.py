import unittest
from filters.crypto_filter import filter_coins

class TestCryptoFilter(unittest.TestCase):

    def test_empty_coins(self):
        coins = []
        required_coins = ['SOL', 'ETH', 'LINK']
        self.assertRaises(ValueError, filter_coins, *[coins, required_coins])

    def test_empty_required_coins(self):
        coins = [{'name': 'Bitcoin', 'symbol': 'BTC', 'price': '$47,200.43', 'change': '-1.5%'}, 
        {'name': 'Ethereum', 'symbol': 'ETH', 'price': '$3,393.91', 'change': '-2.1%'}, 
        {'name': 'Tether', 'symbol': 'USDT', 'price': '$0.999635', 'change': '0.0%'}, 
        {'name': 'BNB', 'symbol': 'BNB', 'price': '$437.85', 'change': '-0.6%'},
        {'name': 'USD Coin', 'symbol': 'USDC', 'price': '$1.00', 'change': '0.1%'},
        {'name': 'XRP', 'symbol': 'XRP', 'price': '$0.862483', 'change': '-2.1%'},
        {'name': 'Cardano', 'symbol': 'ADA', 'price': '$1.19', 'change': '-3.7%'},
        {'name': 'Terra', 'symbol': 'LUNA', 'price': '$106.29', 'change': '2.9%'},
        {'name': 'Solana', 'symbol': 'SOL', 'price': '$112.58', 'change': '-0.6%'},
        {'name': 'Avalanche', 'symbol': 'AVAX', 'price': '$92.54', 'change': '-2.1%'},
        {'name': 'Polkadot', 'symbol': 'DOT', 'price': '$22.26', 'change': '-2.6%'},
        {'name': 'Dogecoin', 'symbol': 'DOGE', 'price': '$0.142914', 'change': '-3.6%'},
        {'name': 'Binance USD', 'symbol': 'BUSD', 'price': '$0.999080', 'change': '-0.0%'},
        {'name': 'TerraUSD', 'symbol': 'UST', 'price': '$0.999840', 'change': '-0.4%'},
        {'name': 'Shiba Inu', 'symbol': 'SHIB', 'price': '$0.000027167441', 'change': '-3.8%'},
        {'name': 'Chainlink', 'symbol': 'LINK', 'price': '$17.16', 'change': '-1.1%'}]
        required_coins = []
        self.assertRaises(ValueError, filter_coins, *[coins, required_coins])
        
    def test_empty_coins_required_coins(self):
        coins = []
        required_coins = []
        self.assertRaises(ValueError, filter_coins, *[coins, required_coins])

    def test_required_coins_filtered(self):
        coins = [{'name': 'Bitcoin', 'symbol': 'BTC', 'price': '$47,200.43', 'change': '-1.5%'}, 
        {'name': 'Ethereum', 'symbol': 'ETH', 'price': '$3,393.91', 'change': '-2.1%'}, 
        {'name': 'Tether', 'symbol': 'USDT', 'price': '$0.999635', 'change': '0.0%'}, 
        {'name': 'Cardano', 'symbol': 'ADA', 'price': '$1.19', 'change': '-3.7%'},
        {'name': 'Terra', 'symbol': 'LUNA', 'price': '$106.29', 'change': '2.9%'},
        {'name': 'Solana', 'symbol': 'SOL', 'price': '$112.58', 'change': '-0.6%'},
        {'name': 'NEAR Protocol', 'symbol': 'NEAR', 'price': '$14.15', 'change': '5.6%'}, 
        {'name': 'Cosmos Hub', 'symbol': 'ATOM', 'price': '$30.09', 'change': '-2.3%'},
        {'name': 'Chainlink', 'symbol': 'LINK', 'price': '$17.16', 'change': '-1.1%'}]

        required_coins = ['SOL', 'ETH', 'LINK']

        filtered_coins = filter_coins(coins, required_coins)

        assert len(filtered_coins) == 3

    def test_required_coins_missing_in_coins(self):
        coins = [{'name': 'Bitcoin', 'symbol': 'BTC', 'price': '$47,200.43', 'change': '-1.5%'}, 
        {'name': 'Ethereum', 'symbol': 'ETH', 'price': '$3,393.91', 'change': '-2.1%'}]

        required_coins = ['SOL', 'ETH', 'LINK']

        filtered_coins = filter_coins(coins, required_coins)

        assert len(filtered_coins) == 1


if __name__ == '__main__':
    unittest.main()
