using Microsoft.Playwright;

namespace PlaywrightTests.Pages
{
    public class FiltersPage
    {
        private readonly IPage _page;
        public FiltersPage(IPage page) => _page = page;
        private ILocator AuthorityFilter => _page.Locator("//button[@name='authority-button']");
        private ILocator CloseAuthorityDropdown => _page.Locator("GetByRole(AriaRole.Button, new() { Name = 'selected'})");
        private Task<IReadOnlyList<ILocator>> Authorities => _page.Locator(".tqc-checkbox__label").AllAsync();
        private ILocator SelectedAuthority => _page.Locator("//div[@class='SelectedTagsstyles__StyledSelectedTags-sc-1r3te2y-0 cYYHej']");
        private Task<IReadOnlyList<ILocator>> SearchOptions => _page.Locator(".LinkBoxstyles__StyledLinkBox-edffol-0 a").AllAsync();
        private ILocator SimpleSearchBar => _page.Locator("[aria-label='Enter your search']");
        private ILocator SearchButton => _page.Locator("[aria-label='Search']");


        public async Task SelectAuthority(string SelectAuthority)
        {
            await AuthorityFilter.ClickAsync();
            var authorities = await Authorities;
            foreach (var element in authorities)
            {
                var authority = await element.InnerTextAsync();
                if (authority.ToLower() == SelectAuthority.ToLower())
                {
                    await element.ClickAsync();
                    break;
                }
            }
            await AuthorityFilter.ClickAsync();
        }

        public async Task<string> IsSelectedAuthorityDisplayed()
        {
            await SelectedAuthority.WaitForAsync();
            return await SelectedAuthority.InnerTextAsync();
        }

        public async Task ClickOnSearchOption(string textToClick)
        {
            // Get all matching elements
            var elements = await SearchOptions;

            foreach (var element in elements)
            {
                // Retrieve the inner text of the element
                var elementText = await element.InnerTextAsync();

                // Check if the element text matches the provided text
                if (elementText.Equals(textToClick, StringComparison.OrdinalIgnoreCase))
                {
                    // Click the element
                    await element.ClickAsync();
                    break; // Exit loop after clicking
                }
            }
        }

        public async Task SearchByPostcode(string searchBy)
        {
            await SimpleSearchBar.FillAsync(searchBy);
            await SearchButton.ClickAsync();
        }

        public async Task<List<string>> GetElementsContainingText(string expectedText)
        {
            var elements = await _page.Locator("p.tqc-text.css-lqlkw3").AllAsync();
            var matchingTexts = new List<string>();

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
                        matchingTexts.Add(text);
                        Console.WriteLine($"Found element with text containing '{expectedText}': {text}");
                    }
                }
            }
            return matchingTexts;
        }
    }
}