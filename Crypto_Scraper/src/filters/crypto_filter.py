def filter_coins(coins, required_coins):
    """Filters coins by symbol"""

    if not required_coins or not coins:
        raise ValueError

    filtered_coins = []
    for coin in coins:
        if coin["symbol"] in required_coins:
            filtered_coins.append(coin)

    return filtered_coins
