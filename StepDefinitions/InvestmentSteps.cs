using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightReqnrollUIDotNet.Drivers;
using PlaywrightReqnrollUIDotNet.Pages;
using static Microsoft.Playwright.Assertions;

namespace PlaywrightReqnrollUIDotNet.StepDefinitions
{
    [Binding]
    public class InvestmentSteps
    {
        private readonly BecuDriver _driver;
        private readonly BecuOrgPage _becuOrgPage;
        private readonly InvestmentPage _investmentPage;

        public InvestmentSteps(BecuDriver becuDriver)
        {
            _driver = becuDriver;
            _becuOrgPage = new BecuOrgPage(_driver.Page);
            _investmentPage = new InvestmentPage(_driver.Page);
        }


        [When("the user selects Investment Services under PLANNING&INVESTING dropdown menu")]
        public async Task WhenTheUserSelectsInvestmentServicesUnderPLANNINGINVESTINGDropdownMenu()
        {
            await _becuOrgPage._planningInvestingMenu.ClickAsync();
            await _becuOrgPage._investmentServicesLink.ClickAsync();
        }

        [Then("Investment Services page is launched")]
        public async Task ThenInvestmentServicesPageIsLaunched()
        {
            Thread.Sleep(10000);
            Assert.That(_driver.Page.Url, Does.Contain(
                "https://www.becu.org/planning-and-investing/personalized-planning/investment-services-overview/investment-services"),
                "Failed to launch Investment Services page");
            Thread.Sleep(6000);
        }

        [When("the user selects LPL Account View button")]
        public async Task WhenTheUserSelectsLPLAccountViewButton()
        {
            await _investmentPage._lplBtn.ClickAsync();
        }

        [Then("Leaving BECU website modal is launched and verified")]
        public async Task ThenLeavingBECUWebsiteModalIsLaunchedAndVerified()
        {
            Thread.Sleep(5000);
            var locatorsOfModal = new List<ILocator>
            {
                _investmentPage._lplModal,       
                _investmentPage._lplModalHeading,
                _investmentPage._lplModalMessage,
                _investmentPage._lplModalFootnote,
               
                _investmentPage._lplModalContinueBtn,
                _investmentPage._lplModalNoThanksBtn,
                _investmentPage._lplModalCloseBtn
            };

            foreach (var locator in locatorsOfModal)
            {
                await Expect(locator).ToBeVisibleAsync();
            }

            await Expect(_investmentPage._lplModalMessage).ToContainTextAsync(
                "Please note: We provide links to other websites for your convenience. " +
                "Please note that linked sites may have a privacy and security policy different from our own, " +
                "and we cannot attest to the accuracy of information.\r\n" +
                "If you wish to leave BECU's Website, select Continue. If not, select No Thanks.");

            await Expect(_investmentPage._lplModalFootnote).ToContainTextAsync(
                "Products and services advertised on these sites " +
                "are offered by independent businesses that are solely responsible for the delivery and " +
                "quality of those products and services. " +
                "BECU does not guarantee nor expressly endorse any particular product or service.");

            await _investmentPage._lplModalCloseBtn.ClickAsync();
            Thread.Sleep(5000);
        }


        [When("the user selects Play video button inside of MoneyGuide Pro section")]
        public async Task WhenTheUserSelectsPlayVideoButtonInsideOfMoneyGuideProSection()
        {
            await _investmentPage._startWithPlanHeading.ScrollIntoViewIfNeededAsync();
            await Expect(_investmentPage._playVideoBtn).ToBeVisibleAsync();
            await _investmentPage._playVideoBtn.ClickAsync();
        }

        [Then("MoneyGuide Pro video is played")]
        public async Task ThenMoneyGuideProVideoIsPlayed()
        {
            Thread.Sleep(3000);
            //simulate a user clicks the center of the video player to pause
            await _investmentPage._playerContent.ClickAsync(new LocatorClickOptions
            {
                Position = new Position { X = 0.5f, Y = 0.5f }
            });
            Thread.Sleep(1000);
            await Expect(_investmentPage._playVideoBtn).ToBeVisibleAsync();
            await _investmentPage._playVideoBtn.ClickAsync();
            Thread.Sleep(1000);
            await Expect(_investmentPage._pauseVideoBtn).ToBeVisibleAsync();
            await _investmentPage._pauseVideoBtn.ClickAsync();
            Thread.Sleep(5000);
        }


    }
}
