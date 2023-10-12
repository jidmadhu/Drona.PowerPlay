using Microsoft.Playwright;

namespace Drona.PowerPlay.PlayWright.Driver
{
    public interface IDriverInitializer
    {
        Task<IBrowser> GetChromeDriverAsync(IBrowserOptions browserOptions);
        Task<IBrowser> GetWebKitDriverAsync(IBrowserOptions browserOptions);
        Task<IBrowser> GetFireFoxDriverAsync(IBrowserOptions browserOptions);
        Task<IBrowser> GetChromiumDriverAsync(IBrowserOptions browserOptions);
    }
    
    public class DriverInitializer: IDriverInitializer
    {
        public async Task<IBrowser> GetChromeDriverAsync(IBrowserOptions browserOptions)
        {
            var options = GetOptions(browserOptions);
            options.Channel = "chrome";
            
            return await GetBrowserAsync(BrowserType.Chromium, options);
        }
        
        public async Task<IBrowser> GetWebKitDriverAsync(IBrowserOptions browserOptions)
        {
            var options = GetOptions(browserOptions);
            options.Channel = "webkit";
            
            return await GetBrowserAsync(BrowserType.Webkit, options);
        }
        
        public async Task<IBrowser> GetFireFoxDriverAsync(IBrowserOptions browserOptions)
        {
            var options = GetOptions(browserOptions);
            options.Channel = "firefox";
            
            return await GetBrowserAsync(BrowserType.Firefox, options);
        }
        
        public async Task<IBrowser> GetChromiumDriverAsync(IBrowserOptions browserOptions)
        {
            var options = GetOptions(browserOptions);
            options.Channel = "chromium";
            
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

