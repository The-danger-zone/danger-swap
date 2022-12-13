import scrapers.scrape_fiat

def get_rates():

    return {"Fiat": scrapers.scrape_fiat.get_data()}
