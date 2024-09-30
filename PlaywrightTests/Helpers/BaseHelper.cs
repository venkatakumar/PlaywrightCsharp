using Microsoft.Playwright;
using System.Collections.Generic;


namespace PlaywrightTests.Helpers
{
    
    public class BaseHelper
    {
        private readonly IPage _page;

        public BaseHelper(IPage page)
        {
            _page = page;
        }
        public async Task NavigateToUrl()
        {
            var baseurl = TestContext.Parameters["BaseUrl"];
            if (string.IsNullOrEmpty(baseurl))
            {
                throw new ArgumentNullException(nameof(baseurl), "Base URL cannot be null or empty.");
            }
            await _page.GotoAsync(baseurl);
        }
    }
}