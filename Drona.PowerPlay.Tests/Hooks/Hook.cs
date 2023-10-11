using System;
using System.Threading.Tasks;
using BoDi;
using Drona.PowerPlay.PlayWright;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace Drona.PowerPlay.Tests.Hooks;

[Binding]
public class Hooks
{
    private readonly DriverManager _driverManager;
    private readonly IObjectContainer _objectContainer;


    public Hooks(DriverManager driverManager, IObjectContainer objectContainer)
    {
        _driverManager = driverManager;
        _objectContainer = objectContainer;
    }


    [BeforeTestRun(Order = 0)]
    public static Task BeforeTestRun(IObjectContainer objectContainer)
    {
        
        var exitCode = Program.Main(new[] { "install" });
        if (exitCode != 0)
        {
            throw new InvalidOperationException($"Playwright exited with code {exitCode}");
        }
        return Task.CompletedTask;
    }

    [BeforeScenario]
    public  Task BeforeScenario()
    {
        try
        {
            var browser = _driverManager.Current;
            
            var context =  browser.Result;
            var page =   context.NewPageAsync();
            _objectContainer.RegisterInstanceAs(page.Result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return Task.CompletedTask;
    }

    
}