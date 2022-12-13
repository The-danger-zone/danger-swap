import time
from utils.get_crypto_rates import get_rates as get_crypto
from utils.get_fiat_rates import get_rates as get_fiat
from parsers.json_parser import parse
from utils.file_writer import write_to_file

if __name__ == "__main__":

    while True:
        try:
            crypto = get_crypto()
            fiat = get_fiat()

            write_to_file(parse({**fiat, **crypto}))

            time.sleep(60)
        except:
            raise TypeError
