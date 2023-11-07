using Drona.PowerPlay.Tests.Pages;
using TechTalk.SpecFlow;

namespace Drona.PowerPlay.Tests.Steps;

[Binding]
public sealed class FormsSteps
{
    private FormsPage _formsPage;

    public FormsSteps(FormsPage formsPage)
    {
        _formsPage = formsPage;
    }

    [Given(@"The User navigate to WebSite")]
    public void GivenTheUserNavigateToWebSite()
    {
        _formsPage.NavigateToPage("https://google.com/");
    }

    [Given(@"The User click on Advocacy under Find Help")]
    public void GivenTheUserClickOnAdvocacyUnderFindHelp()
    {
        _formsPage.clickOnFindHelp();
    }

    [Given(@"The User click on DVA Claims")]
    public void GivenTheUserClickOnDvaClaims()
    {
        
    }

    [When(@"The User fills the DVA Claims Form")]
    public void WhenTheUserFillsTheDvaClaimsForm()
    {
       
    }

    [When(@"The User clicks the Submit button on the DVA Form")]
    public void WhenTheUserClicksTheSubmitButtonOnTheDvaForm()
    {
       
    }

    [Then(@"The User can view ""(.*)""")]
    public void ThenTheUserCanView(string p0)
    { 
    }
}