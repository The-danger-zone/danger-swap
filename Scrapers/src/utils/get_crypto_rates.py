import scrapers.scrape_coingecko
import scrapers.scrape_binance
from filters.filter import filter_currencies

def get_rates():

    required_coins = [{"name": "Bitcoin", "symbol": "BTC", "payload": "BTCUSDT"},
    {"name": "Ethereum", "symbol": "ETH", "payload": "ETHUSDT"},
    {"name": "Cardano", "symbol": "ADA", "payload": "ADAUSDT"},
    {"name": "Polkadot", "symbol": "DOT", "payload": "DOTUSDT"}]

    coins = scrapers.scrape_binance.get_data(required_coins) 

    if not coins:
        coins = filter_currencies(scrapers.scrape_coingecko.get_data(), required_coins)

    return {"Crypto": coins}
