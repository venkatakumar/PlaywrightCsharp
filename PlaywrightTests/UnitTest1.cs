using System.Text.RegularExpressions;
using NUnit.Framework;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Helpers;
using PlaywrightTests.Pages;
using PlaywrightTests.TestData;

namespace PlaywrightTests;

public class Tests : PageTest
{

    [Test]
    public async Task SimpleSearchTest()
    {
        var baseHelper = new BaseHelper(Page);
        await baseHelper.NavigateToUrl();

        await Expect(Page).ToHaveTitleAsync(new Regex("Northern Ireland Public Register"));

        var searchTerm = SampleTestData.SearchTerm;

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
                found = true;
                Console.WriteLine("Found element with text 'bt1'");
            }
        }
        var filters = new FiltersPage(Page);
        await filters.SelectAuthority(SampleTestData.Authority);

        var selectedAuthority = await filters.IsSelectedAuthorityDisplayed();
        Assert.That(selectedAuthority, Is.EqualTo(SampleTestData.Authority), "The selected authority was not displayed correctly.");

        Assert.IsTrue(found, $"Search term '{searchTerm}' not found in any element.");
    }

    [Test]
    public async Task AdvancedSearch()
    {
        var baseHelper = new BaseHelper(Page);
        await baseHelper.NavigateToUrl();

        var searchTerm = SampleTestData.AdvancedSearch_RefNumer;

        var advancedSearch = new AdvancedSearch(Page);
        await advancedSearch.AdvancedSearchByRefNumber(searchTerm);
        await Page.WaitForTimeoutAsync(500);
        await advancedSearch.VerifyElementContainsText(searchTerm);
    }

}