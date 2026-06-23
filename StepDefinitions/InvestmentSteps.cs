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

        [When("the user selects Investment Calculators under PLANNING&INVESTING dropdown menu")]
        public async Task WhenTheUserSelectsInvestmentCalculatorsUnderPLANNINGINVESTINGDropdownMenu()
        {
            await _becuOrgPage._planningInvestingMenu.ClickAsync();
            await _becuOrgPage._investmentCalculatorsLink.ClickAsync();
        }

        [Then("Investment tools and calculators page is launched")]
        public async Task ThenInvestmentToolsAndCalculatorsPageIsLaunched()
        {
            Thread.Sleep(10000);
            Assert.That(_driver.Page.Url, Does.Contain(
                "https://www.becu.org/planning-and-investing/personalized-planning/investment-services-overview/investment-tools-and-calculators"),
                "Failed to launch Investment tools and calculators page");
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


        [When("the user scrolls down to the Calculator of Which will provide the most retirment income and clicks")]
        public async Task WhenTheUserScrollsDownToTheCalculatorOfWhichWillProvideTheMostRetirmentIncomeAndClicks()
        {
            await _investmentPage.expandButton.ScrollIntoViewIfNeededAsync();
            await Task.Delay(3000);
            await _investmentPage.expandButton.ClickAsync();
            await Task.Delay(3000);
        }

        [Then("the bar chart of Which will provide the most retirment income is displayed")]
        public async Task ThenTheBarChartOfWhichWillProvideTheMostRetirmentIncomeIsDisplayed()
        {
            //Verify the default value
            await Expect(_investmentPage.h3Title).ToBeVisibleAsync();
            var h3TitleText = await _investmentPage.h3Title.InnerTextAsync();
            await Task.Delay(6000);
            Assert.That(h3TitleText, Contains.Substring("The Traditional IRA will provide the most retirement income at").IgnoreCase, "h3 title should get Traditional IRA");

            await Expect(_investmentPage.barChart).ToBeVisibleAsync(new() { Timeout = 10000 });
            await Task.Delay(1000);

            //verify total 4 bars
            await Expect(_investmentPage.bars).ToHaveCountAsync(4);
            await Task.Delay(1000);
        }

        [When("the user hovers the first bar of the bar chart")]
        public async Task WhenTheUserHoversTheFirstBarOfTheBarChart()
        {
            //Hover the first bar and show the prompt info
            await _investmentPage.firstBar.HoverAsync();
            //Maintain the hover state for 6 seconds, may not need
            //await _page.WaitForTimeoutAsync(6000);
        }

        [Then("the tooltip of bar chart is displayed")]
        public async Task ThenTheTooltipOfBarChartIsDisplayed()
        {
            await Expect(_investmentPage.tooltip).ToBeVisibleAsync(new() { Timeout = 10000 });
            string? tooltipText = await _investmentPage.tooltip.GetAttributeAsync("aria-label");//working
            Console.WriteLine("tooltipText==>" + tooltipText);
            Assert.That(tooltipText, Is.Not.Null.And.Not.Empty, "Interactive chart tooltip should display details when hovering over a data point.");
            await Task.Delay(1000);
        }

        [When("the user deselects the first bar of the bar chart")]
        public async Task WhenTheUserDeselectsTheFirstBarOfTheBarChart()
        {
            // DeSelect the Show button to hidde the bar    
            await _investmentPage.showTraIraBtn.ScrollIntoViewIfNeededAsync();
            await Task.Delay(1000);
            await _investmentPage.showTraIraBtn.ClickAsync();//to hide the Traditonal IRA bar
            await Task.Delay(1000);
        }

        [Then("the bar of bar chart is hidden")]
        public async Task ThenTheBarOfBarChartIsHidden()
        {
            await Expect(_investmentPage.traIraVar).Not.ToBeVisibleAsync();
        }

        [When("the user selects the button of Show Details")]
        public async Task WhenTheUserSelectsTheButtonOfShowDetails()
        {
            await _investmentPage.showDetailsBtn.ClickAsync();
            Task.Delay(6000);
        }

        [Then("the Details Table of bar chart is displayed")]
        public async Task ThenTheDetailsTableOfBarChartIsDisplayed()
        {
            await Expect(_investmentPage.DetailsTable).ToBeVisibleAsync(new()
            {   //it takes long time for the data table in iFrame to display
                Timeout = 15000
            });

            // Verify the table has data columns, column header           
            var columnCount = await _investmentPage.DetailsTableHeaderLocator.CountAsync();
            Assert.That(columnCount, Is.EqualTo(2), "Results table should contain 2 columns");
            var column1text = await _investmentPage.DetailsTableHeaderLocator.Nth(0).InnerTextAsync();
            Assert.That(column1text, Is.EqualTo("MONTHLY RETIREMENT INCOME"), "Column 1 header is wrong");
            var column2text = await _investmentPage.DetailsTableHeaderLocator.Nth(1).InnerTextAsync();
            Assert.That(column2text, Is.EqualTo("LUMP SUM AT RETIREMENT (BEFORE TAXES)"), "Column 2 header is wrong");

            // Verify the table has data rows, row header
            var rowCount = await _investmentPage.DetailsTableRowsLocator.CountAsync();
            Assert.That(rowCount, Is.EqualTo(4), "Results table should contain 4 rows");
            var row1text = await _investmentPage.DetailsTable.Locator("tbody tr th").Nth(0).InnerTextAsync();
            Assert.That(row1text, Is.EqualTo("TRADITIONAL IRA"), "Row 1 header is wrong");
            var row2text = await _investmentPage.DetailsTable.Locator("tbody tr th").Nth(1).InnerTextAsync();
            Assert.That(row2text, Is.EqualTo("ROTH IRA"), "Row 2 header is wrong");
            var row3text = await _investmentPage.DetailsTable.Locator("tbody tr th").Nth(2).InnerTextAsync();
            Assert.That(row3text, Is.EqualTo("NONDEDUCTIBLE IRA"), "Row 3 header is wrong");
            var row4text = await _investmentPage.DetailsTable.Locator("tbody tr th").Nth(3).InnerTextAsync();
            Assert.That(row4text, Is.EqualTo("TAXABLE ACCOUNT"), "Row 4 header is wrong");

            // Check first row cells contain non-empty text for expected columns
            var firstRowCells = _investmentPage.DetailsTableRowsLocator.Nth(0).Locator("td");
            var cellCount = await firstRowCells.CountAsync();
            Assert.That(cellCount, Is.EqualTo(2), "First row should contain 2 cells");

            // Ensure each cell in the first row has non-empty visible text
            for (int i = 0; i < cellCount; i++)
            {
                var text = (await firstRowCells.Nth(i).InnerTextAsync()).Trim();
                Assert.That(text, Is.Not.Null.And.Not.Empty, $"Cell #{i} in first row should not be empty");
            }
        }


        [When("the user selects the button of View as data table")]
        public async Task WhenTheUserSelectsTheButtonOfViewAsDataTable()
        {
            await _investmentPage.viewAsDataTableBtn.ClickAsync();
            Task.Delay(6000);
        }

        [Then("the Data Table of bar chart is displayed")]
        public async Task ThenTheDataTableOfBarChartIsDisplayed()
        {
            //Table caption
            await Expect(_investmentPage.DataTable).ToBeVisibleAsync(new() { Timeout = 10000 });
            await _investmentPage.DataTable.ScrollIntoViewIfNeededAsync();
            await Task.Delay(10000);
            var captionText = await _investmentPage.DataTable.Locator("> caption").InnerTextAsync();
            Assert.That(captionText, Is.EqualTo("Monthly retirement income provided by the " +
                "Traditional IRA, Roth IRA, Nondeductible IRA and Taxable account"), "Table caption is wrong");

            //thead 5 columns, AriaRole to verify column headers
            var headerCount = await _investmentPage.DataTableHeaderCells.CountAsync();
            Assert.That(headerCount, Is.EqualTo(5), "Data table should contain 5 columns");
            await _investmentPage.DataTable.GetByRole(AriaRole.Columnheader, new() { Name = "Category" }).IsVisibleAsync();
            await _investmentPage.DataTable.GetByRole(AriaRole.Columnheader, new() { Name = "Traditional IRA" }).IsVisibleAsync();
            await _investmentPage.DataTable.GetByRole(AriaRole.Columnheader, new() { Name = "Roth IRA" }).IsVisibleAsync();
            await _investmentPage.DataTable.GetByRole(AriaRole.Columnheader, new() { Name = "Nondeductible IRA" }).IsVisibleAsync();
            await _investmentPage.DataTable.GetByRole(AriaRole.Columnheader, new() { Name = "Taxable Account" }).IsVisibleAsync();

            //tbody 1 row, AriaRole to verify row header

            var rowCount = await _investmentPage.DataTableBodyRows.CountAsync();
            Assert.That(rowCount, Is.GreaterThan(0), "Data table should contain at least 1 row");
            await _investmentPage.DataTable.GetByRole(AriaRole.Rowheader, new() { Name = "Monthly Retirement Income" }).IsVisibleAsync();
            var firstRowCells = _investmentPage.DataTableBodyRows.Nth(0).Locator("td");
            var firstRowCellCount = await firstRowCells.CountAsync();
            //1 rowHeader 4 rowCells
            Assert.That(firstRowCellCount, Is.EqualTo(headerCount - 1), "First row cell count should match header count");

            var bodyCell = _investmentPage.DataTable.Locator(".highcharts-number").First;
            Console.WriteLine("xyz==>" + await bodyCell.InnerTextAsync());

            // Ensure each cell in the first row has non-empty visible text
            for (int i = 0; i < firstRowCellCount; i++)
            {
                var text = (await firstRowCells.Nth(i).InnerTextAsync()).Trim();
                Assert.That(text, Is.Not.Null.And.Not.Empty, $"Cell #{i} in first row should not be empty");
            }

        }

        [When("the user changes the inputs of bar chart")]
        public async Task WhenTheUserChangesTheInputsOfBarChart()
        {
            //Input Contributions    
            await _investmentPage.contributionsBtn.ClickAsync();
            await _investmentPage.thirdPartyIframe.GetByRole(AriaRole.Textbox, new() { Name = "Annual Contribution" }).FillAsync("7000");
            //Certain incorrect implementation in the above step, so have to click the button to display the area again
            await _investmentPage.contributionsBtn.ClickAsync();
            await _investmentPage.thirdPartyIframe.GetByRole(AriaRole.Combobox, new() { Name = "Contributions are invested" }).SelectOptionAsync("After taxes ");
            await Task.Delay(6000);

            //Input About you
            await _investmentPage.aboutYouBtn.ClickAsync();
            await _investmentPage.thirdPartyIframe.GetByLabel("Current age").First.FillAsync("40"); ;
            await _investmentPage.thirdPartyIframe.GetByLabel("Age to begin withdrawing").First.FillAsync("65");
            await Task.Delay(6000);
            //To close the display of the area
            await _investmentPage.aboutYouBtn.ClickAsync();

            //Input Returns
            await _investmentPage.returnsBtn.ScrollIntoViewIfNeededAsync();
            await _investmentPage.returnsBtn.ClickAsync();
            //await Task.Delay(3000);
            await _investmentPage.thirdPartyIframe.GetByRole(AriaRole.Textbox, new() { Name = "Estimated Rate of Return on IRA" }).FillAsync("4");
            await _investmentPage.thirdPartyIframe.GetByRole(AriaRole.Textbox, new() { Name = "Estimated Return on other Assets" }).FillAsync("4");
            //To update the chart  result
            await _investmentPage.thirdPartyIframe.GetByRole(AriaRole.Textbox, new() { Name = "Estimated Return on other Assets" }).PressAsync("Enter");
            await Task.Delay(6000);

        }

        [Then("the result of bar chart is updated")]
        public async Task ThenTheResultOfBarChartIsUpdated()
        {
            //Verify the result
            await _investmentPage.h3Title.ScrollIntoViewIfNeededAsync();
            await Expect(_investmentPage.h3Title).ToBeVisibleAsync();
            var h3TitleText = await _investmentPage.h3Title.InnerTextAsync();
            await Task.Delay(6000);
            Assert.That(h3TitleText, Contains.Substring("The Roth IRA will provide the most retirement income at").IgnoreCase, "h3 title should get Roth IRA");
        }



    }
}
