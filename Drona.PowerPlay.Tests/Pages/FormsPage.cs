using Microsoft.Playwright;

namespace Drona.PowerPlay.Tests.Pages;


public class FormsPage
{
    private readonly IPage _page;

    public FormsPage(IPage page) =>  _page = page;

    public void NavigateToPage(string url)
    {
         _page.GotoAsync(url);
    }

    public void clickOnFindHelp()
    {
        Assertions.Expect(_page.Locator("doishjiofsdjf")).ToBeVisibleAsync();
        _page.Locator("text=FIND HELP").ClickAsync();
       // _page.Locator("css=li[class*='has-submenu']").Nth(0).HoverAsync();
       _page.ClickAsync("css=[href='/find-help/advocacy']");
        _page.Locator("css=[href='/find-help/advocacy']").ClickAsync();
    }
}