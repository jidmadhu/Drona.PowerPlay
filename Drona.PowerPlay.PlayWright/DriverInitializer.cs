using Microsoft.Playwright;

namespace Drona.PowerPlay.PlayWright
{
    public interface IDriverInitializer
    {
        Task<IBrowser> GetChromeDriverAsync(float? timeout = DriverInitializer.DefaultTimeout, bool? headless = true,
            float? slowMotion = null, string? traceDir = null, string[]? args = null);
    }
    
    public class DriverInitializer: IDriverInitializer
    {

        public const float DefaultTimeout = 60f;

        public async Task<IBrowser> GetChromeDriverAsync(float? timeout = DefaultTimeout, bool? headless = true, float? slowMotion = null,
            string? traceDir = null, string[]? args = null)
        {
            var options = GetOptions(timeout, headless, slowMotion, traceDir, args);
            options.Channel = "chrome";
            
            return await GetBrowserAsync(BrowserType.Chromium, options);
        }


        private BrowserTypeLaunchOptions GetOptions(float? timeout = DefaultTimeout, bool? headless = true,
            float? slowMotion = null,
            string? traceDir = null, string[]? args = null)
            => new()
            {
                Args = args,
                Timeout = ToMilliSeconds(timeout),
                Headless = headless,
                SlowMo = slowMotion,
                TracesDir = traceDir
            };

        private async Task<IBrowser> GetBrowserAsync(string browserType, BrowserTypeLaunchOptions options)
        {
            var playwright = await Playwright.CreateAsync();
            return await playwright[browserType].LaunchAsync(options);
        }
        private static float? ToMilliSeconds(float? seconds) => seconds * 1000;

    }
}

