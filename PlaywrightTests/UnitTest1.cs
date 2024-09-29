using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

public class Tests : PageTest
{
    // [SetUp]
    // public async Task Setup()
    // {
    //     await Page.GotoAsync("https://app-planningregister-planningportal.pp.tqinfra.co.uk/simple-search");
    // }

    [TestCase("bt1")]
    public async Task SimpleSearchTest(string searchTerm)
    {
        //BasePage basePage = new BasePage();
        //await basePage.Setup();
        var baseurl = TestContext.Parameters["BaseUrl"];
        await Page.GotoAsync(baseurl);
        await Expect(Page).ToHaveTitleAsync(new Regex("Northern Ireland Public Register"));

        await Page.Locator("[aria-label='Enter your search']").FillAsync(searchTerm);
        await Page.Locator("[aria-label='Search']").ClickAsync();
        await Expect(Page.Locator("[data-testid='search-results-message']")).ToBeVisibleAsync();

        var elements = await Page.Locator("p.tqc-text.css-lqlkw3").ElementHandlesAsync();  

        bool found = false;
        foreach (var element in elements)
        {
            var text = await element.InnerTextAsync();
    
            if (text.ToLower().Contains(searchTerm))
            {
                found =  true;
               Console.WriteLine("Found element with text 'bt1'");
            }
        }
        Assert.IsTrue(found, $"Search term '{searchTerm}' not found in any element.");

    }
}