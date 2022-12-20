using DangerSwap.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using DangerSwap.Interfaces;

namespace DangerSwap.Services;

public sealed class ScrapperService : IScrapperService
{
    private readonly string _installationPath;
    private readonly string _installationStatusFile;
    private readonly string _scrapperInstallerFileName;
    private readonly string _scrapperExecutorFileName;
    private readonly string _executionPath;
    private readonly string _ratesFileName;

    public bool IsRunning { get; private set; }

    public ScrapperService(IConfiguration configurations)
    {
        _installationPath = configurations["ScrappersDependenciesInstallationPath"];
        _executionPath = configurations["ScrapperExecutionPath"];
        _installationStatusFile = _installationPath + "status.txt";
        _scrapperInstallerFileName = configurations["ScrapperInstallerFileName"];
        _scrapperExecutorFileName = configurations["ScrapperExecutorFileName"];
        _ratesFileName = configurations["RatesFileName"];
    }

    public void RunScrappers()
    {
        if (IsRunning)
        {
            return;
        }

        if (!IsInstalled())
        {
            RunScript(_installationPath);
        }
        RunScript(_executionPath, true);
        IsRunning = true;
    }

    private bool IsInstalled()
    {
        const string installedStatus = "installed=1";
        try
        {
            var status = File.ReadAllText(_installationStatusFile);
            return status.Contains(installedStatus);
        }
        catch (Exception)
        {
            return false;
        }
    }

    // Todo: log caught errors
    private void RunScript(string executionPath, bool isScrapper = false)
    {
        var process = new Process();
        process.StartInfo.WorkingDirectory = executionPath;
        process.StartInfo.FileName =
            executionPath + (isScrapper ? _scrapperExecutorFileName : _scrapperInstallerFileName);
        process.StartInfo.CreateNoWindow = false;
        //process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; do not use, untill scrapper dispose functionality is not written
        process.Start();
        if (!isScrapper)
        {
            process.WaitForExit();
        }
    }

    public ScrappedRates ReadScrappedCurrencies()
    {
        var cryptoRatesJson = File.ReadAllText(_executionPath + _ratesFileName);
        var currencies = JsonConvert.DeserializeObject<ScrappedRates>(cryptoRatesJson);
        return currencies ?? new ScrappedRates();
    }
}
