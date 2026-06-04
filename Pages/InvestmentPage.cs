using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

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
        public IFrameLocator _youTubeIframe => _page.FrameLocator("iframe[title='Investments MoneyGuide Pro']");
        public ILocator _playerContent => _youTubeIframe.Locator(".player-controls-content");
        public ILocator _playVideoBtn => _youTubeIframe.GetByRole(AriaRole.Button, new() { Name = "Play video" });//Locator("button[class='icon-button player-control-play-pause-icon'][aria-label='Play video']");  
        public ILocator _pauseVideoBtn => _youTubeIframe.GetByRole(AriaRole.Button, new() { Name = "Pause video" });

        public InvestmentPage(IPage page) => _page = page;



    }
}
