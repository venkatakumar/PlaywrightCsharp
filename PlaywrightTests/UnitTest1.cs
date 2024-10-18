using System.Text.RegularExpressions;
using NUnit.Framework;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Helpers;
using PlaywrightTests.Pages;
using PlaywrightTests.TestData;
using FluentAssertions;
using Microsoft.Playwright;

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

        var simpleSearch = new FiltersPage(Page);
        await simpleSearch.SearchByPostcode(searchTerm);
        await Expect(Page.Locator("[data-testid='search-results-message']")).ToBeVisibleAsync();

        //var filters = new FiltersPage(Page);
        await simpleSearch.SelectAuthority(SampleTestData.Authority);

        var selectedAuthority = await simpleSearch.IsSelectedAuthorityDisplayed();
        Assert.That(selectedAuthority, Is.EqualTo(SampleTestData.Authority), "The selected authority was not displayed correctly.");
        await Page.WaitForTimeoutAsync(500);
        //await Page.WaitForSelectorAsync("//div[@class='results css-0']", new() { State = WaitForSelectorState.Attached });

        var expectedElements = await simpleSearch.GetElementsContainingText(searchTerm);
        expectedElements.Any(a => a.ToLower().Contains(searchTerm.ToLower())).Should()
        .BeTrue($"Expected at least one element to contain the text '{searchTerm}' (case-insensitive), but none were found.");
    }

    [Test]
    public async Task AdvancedSearch()
    {
        var baseHelper = new BaseHelper(Page);
        await baseHelper.NavigateToUrl();

        var search = new FiltersPage(Page);
        await search.ClickOnSearchOption("Advanced search");

        var searchTerm = SampleTestData.AdvancedSearch_RefNumer;

        var advancedSearch = new AdvancedSearch(Page);
        await advancedSearch.AdvancedSearchByRefNumber(searchTerm);
        await Page.WaitForTimeoutAsync(1000);

        // Get the matching elements
        var matchingElements = await advancedSearch.GetElementsContainingText(searchTerm);
        await Page.WaitForTimeoutAsync(200);
        // Assert to ensure at least one element contains the expected text.
        matchingElements.Any(e => e.ToLower().Contains(searchTerm.ToLower())).Should()
        .BeTrue($"Expected at least one element to contain the text '{searchTerm}' (case-insensitive), but none were found.");
        ;
    }
}