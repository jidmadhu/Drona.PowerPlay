using System.Text.Json;
using System.Text.Json.Serialization;
using SpecFlow.Actions.Configuration;

namespace Drona.PowerPlay.PlayWright

{
    public interface IPlayWrightOptions
    {
        Browser Browser { get; }
        
        string[]? Arguments { get; }
        
        float? DefaultTimeout { get; }
        
        bool? Headless { get; }
        
        float? SlowMotion { get; }
        
        string? TraceDir { get; }
    
    }
    
    public class PlayWrightOptions: IPlayWrightOptions
    {

        private readonly ISpecFlowActionJsonLoader _specFlowActionJsonLoader;

        private class SpecflowActionJson
        {
            [JsonInclude]
            public PlayWrightSpecflowJsonPart Playwright { get; private set; } = new();

        }
        
        private class PlayWrightSpecflowJsonPart
        {
            [JsonInclude]
            public Browser Browser { get; private set; }
            
            [JsonInclude]
            public string[]? Arguments { get; private set; }

            [JsonInclude]
            public float? DefaultTimeout { get; private set; }

            [JsonInclude]
            public bool? Headless { get; private set; }

            [JsonInclude]
            public float? SlowMo { get; private set; }

            [JsonInclude]
            public string? TraceDir { get; private set; }
        }

        private readonly Lazy<SpecflowActionJson> _specflowJsonPart;

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

      /// <summary>
      /// Provides the configuration details for the Playwright instance
      /// </summary>
      /// <param name="specFlowActionJsonLoader"></param>
        public PlayWrightOptions(ISpecFlowActionJsonLoader specFlowActionJsonLoader)
        {
            _specFlowActionJsonLoader = specFlowActionJsonLoader;
            _specflowJsonPart = new Lazy<SpecflowActionJson>(LoadSpecflowJson);
        }

      /// <summary>
      /// The browser specified in the configuration
      /// </summary>
        public Browser Browser => _specflowJsonPart.Value.Playwright.Browser;
      /// <summary>
      /// Additional arguments used when launching the browser
      /// </summary>
      public string[]? Arguments => _specflowJsonPart.Value.Playwright.Arguments;

      /// <summary>
      /// The default timeout used to configure the browser
      /// </summary>
      public float? DefaultTimeout => _specflowJsonPart.Value.Playwright.DefaultTimeout;

      /// <summary>
      /// Whether the browser should launch headless
      /// </summary>
      public bool? Headless => _specflowJsonPart.Value.Playwright.Headless;

      /// <summary>
      /// How many miliseconds elapse between every action 
      /// </summary>
      public float? SlowMotion => _specflowJsonPart.Value.Playwright.SlowMo;

      /// <summary>
      /// If specified, traces are saved into this directory 
      /// </summary>
      public string? TraceDir => _specflowJsonPart.Value.Playwright.TraceDir;
    }
}