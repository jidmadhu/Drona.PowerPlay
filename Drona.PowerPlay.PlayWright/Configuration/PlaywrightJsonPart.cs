using System.Text.Json.Serialization;

namespace Drona.PowerPlay.PlayWright.Configuration

{
    public class PlaywrightJsonPart
    {
    
        [JsonInclude]
        public string[]? Arguments { get; private set; }
            
        [JsonInclude]
        public bool? Headless { get; private set; }
            
        [JsonInclude]
        public float? SlowMotion { get; private set; }
    }
}