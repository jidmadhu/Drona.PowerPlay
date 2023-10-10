using System.Threading.Tasks;
using Drona.PowerPlay.Tests.Pages;
using TechTalk.SpecFlow;

namespace Drona.PowerPlay.Tests.Steps;

[Binding]
public class SearchSteps
{
    private readonly SearchPage _searchPage;

    public SearchSteps(SearchPage searchPage) => _searchPage = searchPage;

    [Given(@"I am navigating to (.*)")]
    public async Task GivenIAmNavigatingTo(string url)
    {
        await _searchPage.GotoUrl(url);
    }
}