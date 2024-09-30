using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Helpers;

namespace PlaywrightTests;

public class Tests : PageTest
{

    [TestCase("bt1")]
    public async Task SimpleSearchTest(string searchTerm)
    {
        var baseHelper = new BaseHelper(Page);
        await baseHelper.NavigateToUrl();

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