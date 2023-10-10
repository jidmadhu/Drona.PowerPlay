using Microsoft.Playwright;

namespace Drona.PowerPlay.PlayWright
{
    /// <summary>
    /// Manages the browser instance using playwright
    /// </summary>
    public class DriverManager: IDisposable
    {

        private readonly IPlayWrightOptions _playWrightOptions;
        private readonly DriverInitializer _driverInitializer;
        private readonly AsyncLazy<IBrowser> _currentBrowser;


        private bool _isDisposed;


        public DriverManager(IPlayWrightOptions playWrightOptions, DriverInitializer driverInitializer, AsyncLazy<IBrowser> currentBrowser)
        {
            _playWrightOptions = playWrightOptions;
            _driverInitializer = driverInitializer;
            _currentBrowser = new AsyncLazy<IBrowser>(CreatePlayWrightAsync);
        }

        public Task<IBrowser> Current => _currentBrowser.Value;


        private async Task<IBrowser> CreatePlayWrightAsync()
        {
            return _playWrightOptions.Browser switch
            {
                Browser.Chrome => await _driverInitializer.GetChromeDriverAsync(_playWrightOptions.DefaultTimeout, _playWrightOptions.Headless, _playWrightOptions.SlowMotion, _playWrightOptions.TraceDir, _playWrightOptions.Arguments),
               
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

