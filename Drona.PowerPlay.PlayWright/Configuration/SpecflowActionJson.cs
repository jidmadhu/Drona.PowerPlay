using System.Text.Json.Serialization;

namespace Drona.PowerPlay.PlayWright.Configuration
{
    public class SpecflowActionJson
    {
    
        [JsonInclude] public PlaywrightJsonPart Playwright { get; private set; } = new();
    }
}
