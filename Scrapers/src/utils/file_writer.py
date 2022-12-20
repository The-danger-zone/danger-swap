def write_to_file(data):        
    with open('rates.json', 'w') as file:
        file.write(data)
