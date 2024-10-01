using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightTests.Pages
{
    public class FiltersPage
    {
        private readonly IPage _page;
        public FiltersPage(IPage page) => _page = page;
        private ILocator AuthorityFilter => _page.Locator("//button[@name='authority-button']");
        //private IList<ILocator> Authorities => (IList<ILocator>)_page.Locator(".tqc-checkbox__label");
        private Task<IReadOnlyList<ILocator>> Authorities => _page.Locator(".tqc-checkbox__label").AllAsync();


        public async Task SelectAuthority(string SelectAuthority)
        {
            await AuthorityFilter.ClickAsync();
            var authorities = await Authorities;
            foreach(var element in authorities)
            {
                var authority = await element.InnerTextAsync();
                if (authority.ToLower() == SelectAuthority.ToLower())
                {
                    await element.ClickAsync();
                    break;
                }
            }
        }
    }
}