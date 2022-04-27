def write_to_file(data):
    with open('currency_rates.txt', 'w') as file:
        file.write(data)
