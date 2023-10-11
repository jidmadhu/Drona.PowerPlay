using Microsoft.Playwright;

namespace Drona.PowerPlay.PlayWright
{
    public interface IDriverInitializer
    {
        Task<IBrowser> GetChromeDriverAsync(IBrowserOptions browserOptions);
    }
    
    public class DriverInitializer: IDriverInitializer
    {
        public async Task<IBrowser> GetChromeDriverAsync(IBrowserOptions browserOptions)
        {
            var options = GetOptions(browserOptions);
            options.Channel = "chrome";
            
            return await GetBrowserAsync(BrowserType.Chromium, options);
        }


        private BrowserTypeLaunchOptions GetOptions(IBrowserOptions browserOptions)
            => new()
            {
                Args = browserOptions.Arguments!,
                Headless = browserOptions.Headless,
                SlowMo = browserOptions.SlowMotion,
                Timeout = browserOptions.DefaultTimeout,
                TracesDir = browserOptions.TraceDir,
                DownloadsPath = browserOptions.DownloadPath
            };

        private async Task<IBrowser> GetBrowserAsync(string browserType, BrowserTypeLaunchOptions options)
        {
            var playwright = await Playwright.CreateAsync();
            return await playwright[browserType].LaunchAsync(options);
        }

    }
}

