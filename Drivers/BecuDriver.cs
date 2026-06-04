using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlaywrightReqnrollUIDotNet.Drivers
{
    public class BecuDriver
    {
        private readonly Task<IPage> _page;
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IBrowserContext _browserContext;

        public BecuDriver()
        {
            _page = IntializePlaywright();
        }

        //resolve Task<IPage> to IPage,
        //use it in the steps without worrying about async/await.
        public IPage Page => _page.Result;

        private async Task<IPage> IntializePlaywright()
        {
            //Initialize Playwright
            _playwright = await Playwright.CreateAsync();
            //Browser
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false //headless=true by default, no launch of brower window
            });
            //Page
            _browserContext = await _browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1280, Height = 720 }
            });

            return await _browserContext.NewPageAsync();
        }


        public async Task Dispose(IBrowser _browser)
        {
            await Page.CloseAsync();
            await _browserContext.CloseAsync();
            await _browser.CloseAsync();
        }

    }
}
