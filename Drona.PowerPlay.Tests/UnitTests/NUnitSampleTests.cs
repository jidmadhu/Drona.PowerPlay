using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Drona.PowerPlay.Tests.UnitTests;


public class NUnitSampleTests
{
    [SetUp]
    public Task SetUp()
    {
        int exitCode = Program.Main(new[] { "install" });
        if (exitCode != 0)
        {
            throw new InvalidOperationException($"Playwright exited with code {exitCode}");
        }

        return Task.CompletedTask;
    }

    [Test]
    public async Task Test1()
    {
        //Playwright 
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync( new()
        {
            Headless = false
        });

        var page = await browser.NewPageAsync();
        await page.GotoAsync("https://www.google.com");
        
    }
}