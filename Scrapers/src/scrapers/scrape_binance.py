import json
import requests

def get_data(required_coins):

    BASE_URL = "https://api.binance.com/api/v3/ticker/24hr?symbol="
    full_data = []

    for coin in required_coins:
        endpoint = BASE_URL + coin["payload"]
        data = requests.get(endpoint).json()
        
        full_data.append({"name": coin["name"], "symbol": coin["symbol"], 
        "price": float("{:.2f}".format(float(data["lastPrice"]))), "change": data["priceChangePercent"]})

    return full_data