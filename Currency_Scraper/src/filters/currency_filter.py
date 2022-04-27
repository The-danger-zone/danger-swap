def filter_currency(currency_list, key_currency_values):
    filtered_list = []

    if not currency_list or not key_currency_values:
        raise ValueError
    for currency_item in currency_list:
        for key, value in currency_item.items():
            if value in key_currency_values:
                filtered_list.append(currency_item)
    return filtered_list
