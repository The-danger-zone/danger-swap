@echo off

python -m pip install --upgrade pip
pip install -e .
pip install -r requirements.txt

echo installed=1 > status.txt