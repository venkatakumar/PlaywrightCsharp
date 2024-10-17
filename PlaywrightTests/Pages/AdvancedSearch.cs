using Microsoft.Playwright;

namespace PlaywrightTests.Pages
{
    public class AdvancedSearch
    {
        private readonly IPage _page;
        public AdvancedSearch(IPage page) => _page = page;
        private ILocator ReferenceNumber => _page.GetByLabel("Reference number-input");
        private ILocator SearchButton => _page.GetByRole(AriaRole.Button, new() { Name = "Search", Exact = true });
        private ILocator AdvancedSearchLink => _page.Locator("text='Advanced search'");


        public async Task AdvancedSearchByRefNumber(string searchTerm)
        {
            await AdvancedSearchLink.ClickAsync();
            await ReferenceNumber.ClickAsync();
            await ReferenceNumber.FillAsync(searchTerm);
            await SearchButton.ClickAsync();
        }

        public async Task VerifyElementContainsText(string expectedText)
        {
            // Locate all elements that match the specified class.
            var elements = await _page.Locator("p.tqc-text.css-102diyt").AllAsync();

            // Iterate through each element and check if it contains the expected text.
            bool isTextFound = false;
            foreach (var element in elements)
            {
                // Check if the element is visible
                if (await element.IsVisibleAsync())
                {
                    // Get the inner text of the element
                    var text = await element.InnerTextAsync();

                    // Verify if the text contains the expected substring (case-insensitive)
                    if (text.ToLower().Contains(expectedText.ToLower()))
                    {
                        isTextFound = true;
                        Console.WriteLine($"Found element with text containing '{expectedText}': {text}");
                    }
                }
            }

            // Assert to ensure at least one element contains the expected text.
            Assert.IsTrue(isTextFound, $"No visible elements contain the text '{expectedText}'.");
        }

    }
}
