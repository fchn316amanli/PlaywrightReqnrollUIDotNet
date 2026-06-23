using Microsoft.Playwright;
using NUnit.Framework.Interfaces;

namespace PlaywrightReqnrollUIDotNet.Pages
{
    internal class InvestmentPage
    {
        private readonly IPage _page;
        public ILocator _lplBtn => _page.GetByRole(AriaRole.Link,
                                    new() { Name = "LPL Account view", Description = "LPL Account view", Exact = true });

        // Leaving BECU website modal
        public ILocator _lplModal => _page.Locator(".modal-dialog");
        public ILocator _lplModalHeading => _page.GetByRole(AriaRole.Heading,
                                    new() { Name = "You Are Leaving BECU's Website" });
        public ILocator _lplModalMessage => _page.Locator(".modal-dialog").Locator(".message>p");
        public ILocator _lplModalFootnote => _page.Locator(".modal-dialog").Locator(".component-footnote");
        
        public ILocator _lplModalContinueBtn => _page.GetByRole(AriaRole.Link, new() { Name = "Continue" });
        public ILocator _lplModalNoThanksBtn => _page.GetByRole(AriaRole.Link, new() { Name = "No Thanks", Exact = true });
        public ILocator _lplModalCloseBtn => _page.Locator(".promo-close:has-text('Close')");

        public ILocator _startWithPlanHeading => _page.GetByText("Start with a Plan");

        //IFrameLocator is different than normal ILocator
        //IFrameLocator of video
        public IFrameLocator _youTubeIframe => _page.FrameLocator("iframe[title='Investments MoneyGuide Pro']");
        public ILocator _playerContent => _youTubeIframe.Locator(".player-controls-content");
        public ILocator _playVideoBtn => _youTubeIframe.GetByRole(AriaRole.Button, new() { Name = "Play video" });//Locator("button[class='icon-button player-control-play-pause-icon'][aria-label='Play video']");  
        public ILocator _pauseVideoBtn => _youTubeIframe.GetByRole(AriaRole.Button, new() { Name = "Pause video" });

        //IFrameLocator of bar chart and data tables
        public IFrameLocator thirdPartyIframe => _page.FrameLocator("iframe[title='Roth_Which will provide the most retirement income']");
        public ILocator expandButton => _page.GetByRole(AriaRole.Button, new () { Name = "Which will provide the most retirement income?" });
        public ILocator h3Title => thirdPartyIframe.Locator("#lf_answer");
        public ILocator barChart => thirdPartyIframe.Locator("svg.highcharts-root");
        public ILocator bars => barChart.Locator("g.highcharts-series path");
        public ILocator tooltip => barChart.Locator("path.highcharts-point.highcharts-color-0");
        public ILocator firstBar => barChart.Locator("g.highcharts-series path").Nth(0);
        public ILocator showTraIraBtn => thirdPartyIframe.GetByRole(AriaRole.Button, new() { Name = "Show Traditional IRA" });
        public ILocator traIraVar => barChart.GetByRole(AriaRole.Img, new() { Name = "Monthly Retirement Income, Traditional IRA:" });
        public ILocator showDetailsBtn => thirdPartyIframe.GetByRole(AriaRole.Button, new() { Name = "Show details" }).First;
        public ILocator DetailsTable => thirdPartyIframe.Locator("#lf-results-table-main");
        public ILocator DetailsTableHeaderLocator => DetailsTable.Locator("thead tr th");
        public ILocator DetailsTableRowsLocator => DetailsTable.Locator("tbody tr");
        public ILocator viewAsDataTableBtn => thirdPartyIframe.GetByRole(AriaRole.Button, new() { Name = "View as data table", Exact = true });
        public ILocator DataTable => thirdPartyIframe.Locator("#highcharts-data-table-0");
        public ILocator DataTableHeaderCells => DataTable.Locator(">thead tr th");
        public ILocator DataTableBodyRows => DataTable.Locator(">tbody>tr");
        public ILocator contributionsBtn => thirdPartyIframe.GetByRole(AriaRole.Button, new() { Name = "CONTRIBUTIONS" });
        public ILocator aboutYouBtn => thirdPartyIframe.GetByRole(AriaRole.Button, new() { Name = "ABOUT YOU" });
        public ILocator returnsBtn => thirdPartyIframe.GetByRole(AriaRole.Button, new() { Name = "RETURNS" });




        public InvestmentPage(IPage page) => _page = page;


    }
}
