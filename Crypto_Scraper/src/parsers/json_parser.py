import json

def parse(coins):
    coins_json = json.dumps(coins, indent = 4) 
    return coins_json
