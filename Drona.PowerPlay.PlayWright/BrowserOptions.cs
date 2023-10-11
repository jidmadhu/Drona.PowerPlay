using System.ComponentModel;
using System.Configuration;
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

       


        private const string DefaultBrowserTimeOut = "60";
       
        private static float? ToMilliSeconds(float? seconds) => seconds * 1000;

        public Browser Browser => string.IsNullOrEmpty(ConfigurationManager.AppSettings["Browser"])
            ? Browser.Chrome
            : ConfigurationManager.AppSettings["Browser"] switch
            {
                "chrome" => Browser.Chrome,
                "edge" => Browser.Edge,
                "firefox" => Browser.Firefox,
                "chromium" => Browser.Chromium,
                "webkit" => Browser.Webkit,
                _ => throw new InvalidEnumArgumentException("Browser Not Supported. Supported Browsers are chrome, edge, firefox, chromium, webkit")
            };

        public string[]? Arguments => _specflowJsonPart.Value.Playwright.Arguments;

        public float? DefaultTimeout => ToMilliSeconds(float.Parse(ConfigurationManager.AppSettings["Timeout"] ?? DefaultBrowserTimeOut));
        public bool? Headless => _specflowJsonPart.Value.Playwright.Headless;
        public float? SlowMotion => ToMilliSeconds(float.Parse(ConfigurationManager.AppSettings["SlowMotion"] ?? "0"));
        public string? TraceDir => ConfigurationManager.AppSettings["TraceDir"];

        public string? DownloadPath => ConfigurationManager.AppSettings["DownloadPath"];
    }
}

