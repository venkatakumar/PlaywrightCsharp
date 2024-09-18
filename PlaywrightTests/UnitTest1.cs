using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

public class Tests : PageTest
{
    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync("https://app-planningregister-planningportal.pp.tqinfra.co.uk/simple-search");
    }

    [Test]
    public async Task SimpleSearchTest()
    {
        await Expect(Page).ToHaveTitleAsync(new Regex("Northern Ireland Public Register"));
        //await Page.ClickAsync(selector:"text=Advanced search");
        await Page.Locator("[aria-label='Enter your search']").FillAsync("bt1");
        await Page.Locator("[aria-label='Search']").ClickAsync();
        await Expect(Page.Locator("[data-testid='search-results-message']")).ToBeVisibleAsync();
    }
}