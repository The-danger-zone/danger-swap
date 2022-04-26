def filter_currency(list, key_currency_values):
    filtered_list = []

    for item in list:
        for key, value in item.items():
            if value in key_currency_values:
                filtered_list.append(item)
    return filtered_list
