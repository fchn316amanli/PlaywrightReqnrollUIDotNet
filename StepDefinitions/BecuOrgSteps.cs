using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightReqnrollUIDotNet.Drivers;
using PlaywrightReqnrollUIDotNet.Pages;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.Playwright.Assertions;

namespace PlaywrightReqnrollUIDotNet.StepDefinitions
{
    [Binding]
    public class BecuOrgSteps
    {
        private readonly BecuDriver _driver;
        private readonly BecuOrgPage _becuOrgPage;

        public BecuOrgSteps(BecuDriver becuDriver)
        {
            _driver = becuDriver;
            _becuOrgPage = new BecuOrgPage(_driver.Page);
        }

        [Given("a user navigates to BECU home page")]
        public async Task GivenAUserNavigatesToBECUhomPage()
        {
            Console.WriteLine("a user navigates to BECU home page");
            await _driver.Page.GotoAsync("https://www.becu.org/", 
                new PageGotoOptions { Timeout = 90000 });
            Thread.Sleep(1000);

            if (await _becuOrgPage._cookieOverlay.IsVisibleAsync())
            {
                await _becuOrgPage._cookieCloseBtn.ClickAsync();
            }
        }

        [When("the user clicks BECU logo")]
        public async Task WhenTheUserClicksBECULogo()
        {
            await _becuOrgPage._logo.ClickAsync();
        }

        private async Task VerifyNavbar()
        {
            var locatorsOfNavbar = new List<ILocator>
            {
                _becuOrgPage._logo,
                _becuOrgPage._locationIcon,
                _becuOrgPage._supportIcon,
                _becuOrgPage._loginBtn,
                _becuOrgPage._JoinBtn
            };

            foreach (var locator in locatorsOfNavbar)
            {
                //Playwright buit-in assertions
                await Expect(locator).ToBeVisibleAsync();
                await Expect(locator).ToBeEnabledAsync();
            }   
        }

        [Then("BECU home page is refreshed with navbar displayed")]
        public async Task ThenBECUHomePageIsRefreshedWithNavbarDisplayed()
        {
            Thread.Sleep(10000);//Wait for 10 seconds 
            //NUnit test assertion to verify the URL with error message if the assertion fails
            Assert.That(_driver.Page.Url, Is.EqualTo("https://www.becu.org/"), 
                "Failed to go back to home page");
            Thread.Sleep(6000);
            await VerifyNavbar();
        }

        [When("the user clicks EVERYDAY BANKING dropdown menu")]
        public async Task WhenTheUserClicksEVERYDAYBANKINGDropdownMenu()
        {
            await _becuOrgPage._everydayBankingMenu.ClickAsync();
        }

        [Then("savings shares are displayed")]
        public async Task ThenSavingsSharesAreDisplayed()
        {
            var locatorsOfSavingsShares = new List<ILocator>
            {
                _becuOrgPage._savingsLink,
                _becuOrgPage._youthSavingsLink,
                _becuOrgPage._moneyMarketLink,
                _becuOrgPage._cdLink,
                _becuOrgPage._iraLink
            };

            await Expect(_becuOrgPage._savingsHeading).ToBeVisibleAsync();
            //Loop and assert each share link
            foreach (var locator in locatorsOfSavingsShares)
            {
                await Expect(locator).ToBeVisibleAsync();
                await Expect(locator).ToBeEnabledAsync();
            }
        }

        [When("the user plays a button of slide on featured promotions slider")]
        public async Task WhenTheUserPlaysAButtonOfSlideOnFeaturedPromotionsSlider()
        {
            await _becuOrgPage._carouselSlideBtn3of4.ClickAsync();
        }

        [Then("the slides of featured promotions slider are displayed properly")]
        public async Task ThenTheSlidesOfFeaturedPromotionsSliderAreDisplayedProperly()
        {
            // Verify Carousel Slides
            // 1. Verify Carousel container
            await Expect(_becuOrgPage._carousel).ToBeVisibleAsync();

            // 2. Convert multiple locators into List, then verify slides count
            var slideList = await _becuOrgPage._carouselSlideList.AllAsync();
            int slideCount = slideList.Count();
            Console.WriteLine($"===>Total slides found: {slideCount}");
            Assert.That(slideCount, Is.EqualTo(4), "slides count in the carousel is wrong");

            // 3. Verify carousel buttons
            await Expect(_becuOrgPage._carouselPrevSlideBtn).ToBeVisibleAsync();
            await Expect(_becuOrgPage._carouselPrevSlideBtn).ToBeEnabledAsync();
            await Expect(_becuOrgPage._carouselNextSlideBtn).ToBeVisibleAsync();
            await Expect(_becuOrgPage._carouselNextSlideBtn).ToBeEnabledAsync();
            var slideBtnList = await _becuOrgPage._carouselSlideBtnList.AllAsync();
            int slideBtnCount = slideBtnList.Count();
            Console.WriteLine($"===>Total slide buttons found: {slideBtnCount}");
            Assert.That(slideBtnCount, Is.EqualTo(slideCount), "slide button count should be equal to slide count");

            // 4. Verify selected slide is active (visible)
            await Expect(_becuOrgPage._carouselSlideActive).ToHaveAttributeAsync("aria-hidden", "false");

            // 5. Verify pause and play buttons
            await _becuOrgPage._carouselPauseBtn.ClickAsync();
            Thread.Sleep(1000);
            await _becuOrgPage._carouselPlayBtn.ClickAsync();
            Thread.Sleep(8000);
            await Expect(_becuOrgPage._carouselSlideActive).ToHaveAttributeAsync("aria-hidden", "false");

            // 6. Verify Next slide and Previous slide buttons
            await _becuOrgPage._carouselNextSlideBtn.ClickAsync();
            await Expect(_becuOrgPage._carouselSlideActive).ToHaveAttributeAsync("aria-hidden", "false");
            Thread.Sleep(6000);
            await _becuOrgPage._carouselPrevSlideBtn.ClickAsync();
            Thread.Sleep(6000);
            await Expect(_becuOrgPage._carouselSlideActive).ToHaveAttributeAsync("aria-hidden", "false");
        }

        [When("the user selects Business Banking on secondary-promo")]
        public async Task WhenTheUserSelectsBusinessBankingOnSecondary_Promo()
        {
            // Explicitly scroll it into view
            await _becuOrgPage._bizBankingHeading.ScrollIntoViewIfNeededAsync();
            Thread.Sleep(2000);
            string bizBankingPromoDetails = await _becuOrgPage._bizBankingPromoDetails.InnerTextAsync();
            string expectedBizText = "Explore how BECU's business products and services can help power your business.";
            Assert.That(bizBankingPromoDetails, Is.EqualTo(expectedBizText), "Business Banking promo details is wrong");

            await _becuOrgPage._bizBankingHeading.ClickAsync();
        }

        [Then("Business Banking page is launched")]
        public async Task ThenBusinessBankingPageIsLaunched()
        {
            Thread.Sleep(10000);
            Assert.That(_driver.Page.Url, Is.EqualTo("https://www.becu.org/business-banking"),
                "Failed to launch Business Banking page");
            Thread.Sleep(6000);
        }

        private async Task VerifyFooterMenuOptions()
        {
            var locatorsOfFooterMenu = new List<ILocator>
            {
                //To-do to expend to all menu options in the footer
                _becuOrgPage._accessibilityFooterMenu,
                _becuOrgPage._contactFooterMenu
            };
            foreach (var locator in locatorsOfFooterMenu)
            {
                //Playwright buit-in assertions
                await Expect(locator).ToBeVisibleAsync();
                await Expect(locator).ToBeEnabledAsync();
            }
        }

        private async Task VerifyFooterCopyrightList()
        {
            var copyrightList = await _becuOrgPage._footerCopyrightList.AllAsync();
            int count = copyrightList.Count();
            Console.WriteLine($"===>Total footer copyright items: {count}");
            List<string> copyrightTexts = new List<string> {
                "© 2026 BECU.  All Rights Reserved.",
                "Boeing Employees' Credit Union NMLS ID 490518",
                "Federally Insured by NCUA.",
                "Equal Housing Opportunity Lender"
            };

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"===>for loop int: {i}");
                var currentItem = _becuOrgPage._footerCopyrightList.Nth(i);
                await Expect(currentItem).ToBeVisibleAsync();
                await Expect(currentItem).ToContainTextAsync(copyrightTexts[i]);
            }
        }

        private async Task VerifyFooterSocialMediaList()
        {
            var socialMediaList = await _becuOrgPage._footerSocialList.AllAsync();
            int count = socialMediaList.Count();
            Console.WriteLine($"===>Total footer social media items: {count}");
            List<string> socialMediaLTitles = new List<string> {
                "BECU's Facebook page",
                "BECU's Instagram page",
                "BECU's Twitter feed",
                "BECU's LinkedIn page",
                "BECU's YouTube page"
            };
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"===>for loop int: {i}");
                var currentItem = _becuOrgPage._footerSocialList.Nth(i);
                await Expect(currentItem).ToBeVisibleAsync();
                await Expect(currentItem).ToBeEnabledAsync();
                await Expect(currentItem.Locator(">a")).ToHaveAttributeAsync("title", socialMediaLTitles[i]);
            }
        }

        [When("the user selects Contact on footer menu")]
        public async Task WhenTheUserSelectsContactOnFooterMenu()
        {
            // 1. Explicitly scroll it into view, verify footer menu options
            await _becuOrgPage._accessibilityFooterMenu.ScrollIntoViewIfNeededAsync();
            await VerifyFooterMenuOptions();

            // 2. Verify footer copyright list
            await VerifyFooterCopyrightList();

            // 3. Verify footer social media list
            await VerifyFooterSocialMediaList();

            Thread.Sleep(6000);
            await _becuOrgPage._contactFooterMenu.ClickAsync();
        }

        [Then("Contact page is launched")]
        public async Task ThenContactPageIsLaunched()
        {
            Thread.Sleep(10000);
            Assert.That(_driver.Page.Url, Does.Contain("https://www.becu.org/support/contact-us"),
                "Failed to launch Contact page");
            Thread.Sleep(6000);
        }




    }
}
