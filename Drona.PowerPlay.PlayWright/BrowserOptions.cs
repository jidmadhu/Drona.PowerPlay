
#nullable enable
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Drona.PowerPlay.PlayWright.Configuration;
using SpecFlow.Actions.Configuration;

namespace Drona.PowerPlay.PlayWright
{
    public interface IBrowserOptions
    {
        Browser Browser { get; }
        
        string[]? Arguments { get; }
        
        float? DefaultTimeout { get; }
        
        bool? Headless { get; }
        
        float? SlowMotion { get; }
        
        string? TraceDir { get; }
        
        string? DownloadPath { get; }
    
    }
    public class BrowserOptions: IBrowserOptions
    {
        
        //Reading from the specflow.action.json
        private readonly ISpecFlowActionJsonLoader _specFlowActionJsonLoader;
        private readonly Lazy<SpecflowActionJson> _specflowJsonPart;

        
        /// <summary>
        /// Provides the configuration details for the Playwright instance
        /// </summary>
        /// <param name="specFlowActionJsonLoader"></param>
        public BrowserOptions(ISpecFlowActionJsonLoader specFlowActionJsonLoader)
        {
            _specFlowActionJsonLoader = specFlowActionJsonLoader;
            _specflowJsonPart = new Lazy<SpecflowActionJson>(LoadSpecflowJson);
        }
        
       private SpecflowActionJson LoadSpecflowJson()
        {
            var json = _specFlowActionJsonLoader.Load();

            if (string.IsNullOrWhiteSpace(json))
            {
                return new SpecflowActionJson();
            }

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            var specflowActionConfig = JsonSerializer.Deserialize<SpecflowActionJson>(json, jsonSerializerOptions);

            return specflowActionConfig ?? new SpecflowActionJson();
        }

        private static float? ToMilliSeconds(float? seconds) => seconds * 1000;

        public Browser Browser => string.IsNullOrEmpty(_specflowJsonPart.Value.Playwright.Browser)
            ? Browser.Chrome
            : _specflowJsonPart.Value.Playwright.Browser switch
            {
                "chrome" => Browser.Chrome,
                "firefox" => Browser.Firefox,
                "chromium" => Browser.Chromium,
                "webkit" => Browser.Webkit,
                _ => throw new InvalidEnumArgumentException("Browser Not Supported. Supported Browsers are chrome, edge, firefox, chromium, webkit")
            };

        public string[]? Arguments => _specflowJsonPart.Value.Playwright.Arguments;

        public float? DefaultTimeout => ToMilliSeconds(_specflowJsonPart.Value.Playwright.DefaultTimeout);
        public bool? Headless => _specflowJsonPart.Value.Playwright.Headless;
        public float? SlowMotion => ToMilliSeconds(_specflowJsonPart.Value.Playwright.SlowMotion);
        public string? TraceDir => _specflowJsonPart.Value.Playwright.TraceDir;

        public string? DownloadPath => _specflowJsonPart.Value.Playwright.DownloadPath;
    }
}

