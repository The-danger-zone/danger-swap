def currency_filter(dictionary, keyCurrencyValues):
    filtered_dict = {}

    for key, values in dictionary.items():
        if key in keyCurrencyValues:
            filtered_dict[key] = values

    return filtered_dict
