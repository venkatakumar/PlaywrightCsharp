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
        private ILocator SearchElements => _page.Locator("//div[@class='filter-panel expanded css-pjcyw6']/div[1]/div/a");



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

        public async Task ClickOnFilterOption(string textToClick)
        {
            // Get all matching elements
            var elements = await SearchElements.ElementHandlesAsync();

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
    }
}