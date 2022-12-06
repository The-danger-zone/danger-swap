def filter_currencies(currencies, required_currencies):
    """Filters currency by symbol"""

    if not required_currencies or not currencies:
        raise ValueError

    filtered_currencies = [currency for currency in currencies if currency["symbol"] in required_currencies]
    return filtered_currencies
