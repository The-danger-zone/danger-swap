using DangerSwap.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DangerSwap.Services
{
    public class ScrapperService
    {
        private readonly ConfigurationManager _configurations;
        private readonly string _installationPath = string.Empty;
        private readonly string _installationStatusFile = string.Empty;
        private readonly string _scrapperInstallerFileName = string.Empty;
        private readonly string _scrapperExecutorFileName = string.Empty;
        private readonly string _cryptoRatesFileName = string.Empty;
        private readonly string _fiatRatesFileName = string.Empty;
        private readonly Tuple<string, string> _executionPaths;

        public ScrapperService(ConfigurationManager configurations)
        {
            _configurations = configurations;
            _installationPath = _configurations["ScrappersDependenciesInstallationPath"];
            _executionPaths = new Tuple<string, string>(
                _configurations["CryptoScrapperExecutionPath"], 
                _configurations["FiatScrapperExecutionPath"]);
            _installationStatusFile = _installationPath + "status.txt";
            _scrapperInstallerFileName = _configurations["ScrapperInstallerFileName"];
            _scrapperExecutorFileName = _configurations["ScrapperExecutorFileName"];
            _cryptoRatesFileName = _configurations["CryptoRatesFileName"];
            _fiatRatesFileName = _configurations["FiatRatesFileName"];
        }

        public void RunScrappers()
        {
            if (!IsInstalled())
            {
                RunScript(_installationPath);
            }
            // Crypto
            RunScript(_executionPaths.Item1, true);
            // Fiat
            //RunScript(_executionPaths.Item2);
        }

        private bool IsInstalled()
        {
            try
            {
                string status = File.ReadAllText(_installationStatusFile);
                return status.Contains("installed=1");
            }
            catch (Exception)
            {
                return false;
            }
        }
        // Todo: log caught errors
        private bool RunScript(string executionPath, bool isScrapper = false)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.WorkingDirectory = executionPath;
                process.StartInfo.FileName = executionPath + (isScrapper ? _scrapperExecutorFileName : _scrapperInstallerFileName);
                process.StartInfo.CreateNoWindow = false;
                //process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; do not use, untill scrapper dispose functionality is not written
                process.Start();
                if (!isScrapper)
                    process.WaitForExit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
        public IEnumerable<ScrappedCurrency> ReadScrappedCurrencies(bool isFiat)
        {
            try
            {
                string cryptoRatesJson = File.ReadAllText(isFiat ? _executionPaths.Item2 + _fiatRatesFileName :
                    _executionPaths.Item1 + _cryptoRatesFileName);
                IEnumerable<ScrappedCurrency> currencies = JsonConvert.DeserializeObject<IEnumerable<ScrappedCurrency>>(cryptoRatesJson);
                return currencies;
            } catch (Exception)
            {
                return Enumerable.Empty<ScrappedCurrency>();
            }
        }
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    }
}
