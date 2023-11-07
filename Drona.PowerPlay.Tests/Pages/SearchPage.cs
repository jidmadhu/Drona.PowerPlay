using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace Drona.PowerPlay.Tests.Pages;

public class SearchPage
{
    private readonly IPage _page;

    public SearchPage(IPage page) => _page = page;

    public async Task GotoUrl(string url)
    {
        await _page.GotoAsync(url);
    }
}