using System;
using System.Threading.Tasks;
using BoDi;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace Drona.PowerPlay.Tests.Hooks;

[Binding]
public class Hooks
{
    private static IObjectContainer _container;

    private readonly IObjectContainer _objectContainer;

    public Hooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }


    [BeforeTestRun(Order = 0)]
    public static async Task BeforeTestRun(IObjectContainer objectContainer)
    {
        
        var exitCode = Program.Main(new[] { "install" });
        if (exitCode != 0)
        {
            throw new InvalidOperationException($"Playwright exited with code {exitCode}");
        }

        //Playwright
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new ()
        {
            Headless = false
        });
        
        objectContainer.RegisterInstanceAs(browser);
        _container = objectContainer;
    }

    [BeforeScenario()]
    public async Task BeforeScenario()
    {
        try
        {
            var browser = _container.Resolve<IBrowser>();
            
            var context = await browser.NewContextAsync();
            var page =  context.NewPageAsync();
            _objectContainer.RegisterInstanceAs(page.Result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [AfterScenario(Order = 0)]
    public void Dispose()
    {
        var page = _objectContainer.Resolve<IPage>();
        page.CloseAsync();
    }
    
}