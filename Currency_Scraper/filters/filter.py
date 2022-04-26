def filter_currency(list, key_currency_values):
    filtered_list = []

    for item in list:
        if list["name"] in key_currency_values:
            filtered_list.append(item)
    return filtered_list