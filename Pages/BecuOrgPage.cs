using Microsoft.Playwright;
using PlaywrightReqnrollUIDotNet.Drivers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace PlaywrightReqnrollUIDotNet.Pages
{
    internal class BecuOrgPage
    {
        private readonly IPage _page;

        public ILocator _cookieOverlay => _page.Locator("[id='truste-consent-content']");
        public ILocator _cookieCloseBtn => _page.GetByLabel("Close cookie notification");
        //navbar
        public ILocator _logo => _page.GetByAltText("BECU Logo");
        public ILocator _searchIcon => _page.GetByAltText("Global Search icon");
        public ILocator _locationIcon => _page.GetByRole(AriaRole.Link, new() { Name = "Locations" });
        public ILocator _supportIcon => _page.Locator("[class*='navbar']").GetByRole(AriaRole.Link, new() { Name = "Support" });
        public ILocator _loginBtn => _page.GetByRole(AriaRole.Link, new() { Name = "Log In" });
        public ILocator _JoinBtn => _page.GetByRole(AriaRole.Link, new() { Name = "Join BECU" });
        public ILocator _everydayBankingMenu => _page.GetByRole(AriaRole.Link, new() { Name = "Everyday Banking" });
        //dropdown-toggle menu items under Everyday Banking
        public ILocator _savingsHeading => _page.Locator("[class*='mega-dropdown-menu show']")
                                                .Locator(".head").Filter(new() { HasText = "Savings" });
        public ILocator _savingsLink => _page.GetByRole(AriaRole.Link, 
                                            new() { Name = "Savings", Description = "Savings", Exact = true });
        public ILocator _youthSavingsLink => _page.GetByRole(AriaRole.Link, new() { Name = "Youth Savings" });
        public ILocator _moneyMarketLink => _page.GetByRole(AriaRole.Link, 
                                            new() { Name = "Money Market", Description = "Money Market", Exact = true });
        public ILocator _cdLink => _page.GetByRole(AriaRole.Link, new() { Name = "CD" });
        public ILocator _iraLink => _page.GetByRole(AriaRole.Link, new() { Name = "IRA" });
        
        public ILocator _planningInvestingMenu => _page.GetByRole(AriaRole.Link, new() { Name = "Planning & Investing" });
        public ILocator _investmentServicesLink => _page.GetByRole(AriaRole.Link,
                                    new() { Name = "Investment Services", Description = "Investment Management", Exact = true });




        //Carousel
        public ILocator _carousel => _page.GetByRole(AriaRole.Region, 
                                    new() { Name = "Featured promotions slider" });
        //Multiple locators in the slide list, but only 4 of them are in use
        //or Locator("[aria-describedby*='slick-slide10']")
        public ILocator _carouselSlideList => _page.Locator("[class*='slick-slide']").Locator("[id*='slick-slide0']");
        public ILocator _carouselPrevSlideBtn => _page.GetByRole(AriaRole.Button, new() { Name = "Previous slide" } );
        public ILocator _carouselNextSlideBtn => _page.GetByRole(AriaRole.Button, new() { Name = "Next slide" });
        public ILocator _carouselPlayBtn => _page.GetByRole(AriaRole.Img, new() { Name = "Play Slides" });
        public ILocator _carouselPauseBtn => _page.GetByRole(AriaRole.Img, new() { Name = "Pause Slides" });
        public ILocator _carouselSlideBtnList => _page.Locator("[class='slick-dots']").Locator("[aria-controls*='slick-slide0']");
        public ILocator _carouselSlideBtn3of4 => _page.GetByTitle("slide 3 of 4");
        public ILocator _carouselSlideActive => _page.Locator("[class*='slick-active slick-center']");

        //secondary-promo
        public ILocator _bizBankingHeading => _page.GetByRole(AriaRole.Heading, new() { Name = "Business Banking" });
        public ILocator _bizBankingPromoDetails => _page.GetByLabel("Explore how BECU's business products").Locator(".promo-details");

        //footer mneu options
        public ILocator _accessibilityFooterMenu => _page.Locator("[id='standardFooter']").GetByRole(AriaRole.Link, new() { Name = "Accessibility" });
        public ILocator _contactFooterMenu => _page.Locator("[id='standardFooter']").GetByRole(AriaRole.Link, new() { Name = "Contact" });
        public ILocator _footerCopyrightList => _page.Locator(".footer-copyright").Locator("li > p");//paragraph under Listitem
        public ILocator _footerSocialList => _page.Locator(".footer-social").Locator("ul > li");


        //Constructor can be simplified to an expression-bodied constructor,
        //no need to use {} and return statement, more concise and readable.
        public BecuOrgPage(IPage page) => _page = page;

        //To-do
        public async Task ClickLoginBtn() => await _loginBtn.ClickAsync();
        //To-do
        public async Task example(string username, string password)
        {
            /*
            await _usernameInput.FillAsync(username);
            await _passwordInput.FillAsync(password);
            await _signInBtn.ClickAsync();
            */
        }

    }
}
