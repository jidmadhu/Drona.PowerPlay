using System.Configuration;
using Microsoft.Playwright;

namespace Drona.PowerPlay.PlayWright
{
    /// <summary>
    /// Manages the browser instance using playwright
    /// </summary>
    public class DriverManager: IDisposable
    {
        private readonly IBrowserOptions _browserOptions;
        private readonly IDriverInitializer _driverInitializer;
        private readonly AsyncLazy<IBrowser> _currentBrowser;


        private bool _isDisposed;


        public DriverManager(BrowserOptions browserOptions, DriverInitializer driverInitializer)
        {
            _browserOptions = browserOptions;
            _driverInitializer = driverInitializer;
            _currentBrowser = new AsyncLazy<IBrowser>(CreatePlayWrightAsync);
        }

        public Task<IBrowser> Current => _currentBrowser.Value;


        private async Task<IBrowser> CreatePlayWrightAsync()
        {
            return _browserOptions.Browser switch
            {
                Browser.Chrome => await _driverInitializer.GetChromeDriverAsync(_browserOptions),
               
            };
        }
        
      public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentBrowser.IsValueCreated)
            {
                Task.Run(async delegate
                {
                    await (await Current).CloseAsync();
                    await (await Current).DisposeAsync();
                });
            }

            _isDisposed = true;
        }
    }
}

