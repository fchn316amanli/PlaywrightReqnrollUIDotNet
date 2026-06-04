using Microsoft.Playwright;
using NUnit.Framework;
using Reqnroll.Assist.Dynamic;
using PlaywrightReqnrollUIDotNet.Drivers;
using PlaywrightReqnrollUIDotNet.Pages;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.Playwright.Assertions;   

namespace PlaywrightReqnrollUIDotNet.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly BecuDriver _driver;
        private readonly BecuOrgPage _becuOrgPage;
        private readonly LoginPage _loginPage;

        public LoginSteps(BecuDriver becuDriver)
        {
            _driver = becuDriver;
            _becuOrgPage = new BecuOrgPage(_driver.Page);
            _loginPage = new LoginPage(_driver.Page);
        }

        [When("the user clicks Login button on the top right corner of home page")]
        public async Task WhenTheUserClicksLoginButtonOnTheTopRightCornerOfHomePage()
        {
            await _becuOrgPage._loginBtn.ClickAsync();
        }

        private async Task VerifyLoginPageBasic()
        {
            var locatorsOfNavbar = new List<ILocator>
            {
                _loginPage._pageHeading1,
                _loginPage._pageHeading2,
                _loginPage._userIdLabel,
                _loginPage._passwordLabel,
                _loginPage._loginBtn,
                _loginPage._forgetPasswordLnk,
                _loginPage._forgetUserIdLnk,
                _loginPage._alreadyMemberLabel,
                _loginPage._notEnrollYetText,
                _loginPage._toEnrollLnk,
                _loginPage._notMemberLabel,
                _loginPage._signUpMembershipLnk,

                _loginPage._pageHeading2Biz,
                _loginPage._forLLCText,
                _loginPage._bizOnlineBankingBtn,
                _loginPage._enrollBizOnlineBankingBtn
            };

            foreach (var locator in locatorsOfNavbar)
            {
                await Expect(locator).ToBeVisibleAsync();
            }
        }

        [Then("Login page is launched and verified")]
        public async Task ThenLoginPageIsLaunchedAndVerified()
        {
            Thread.Sleep(10000);
            Assert.That(_driver.Page.Url, Is.EqualTo("https://auth.secure.becu.org/"),
                "Failed to launch login page");
            Thread.Sleep(6000);

            await VerifyLoginPageBasic(); 
        }


        [When("the user inputs the following fake username and password")]
        public async Task WhenTheUserInputsTheFollowingFakeUsernameAndPassword(DataTable dataTable)
        {
            dynamic loginData = dataTable.CreateDynamicInstance();
            Console.WriteLine("username==>{0}", loginData.Username);
            Console.WriteLine("password==>{0}", loginData.Password);    
            await _loginPage._usernameInput.FillAsync(loginData.Username);
            await _loginPage._passwordInput.FillAsync(loginData.Password);
            await _loginPage._loginBtn.ClickAsync();
        }

        [Then("error message is displayed")]
        public async Task ThenErrorMessageIsDisplayed(string multilineText)
        {
            await Expect(_loginPage._errorMessage).ToBeVisibleAsync();
            await Expect(_loginPage._errorMessage).ToHaveTextAsync(multilineText);
        }

        [When("the user inputs blank username and password")]
        public async Task WhenTheUserInputsBlankUsernameAndPassword()
        {
            await _loginPage._usernameInput.FillAsync("");
            await _loginPage._passwordInput.FillAsync("");
            await _loginPage._loginBtn.ClickAsync();
        }

        [Then("user ID and password error messages are displayed as below")]
        public async Task ThenUserIDAndPasswordErrorMessagesAreDisplayedAsBelow(DataTable dataTable)
        {
            dynamic errorData = dataTable.CreateDynamicInstance();
            Console.WriteLine("userIdError==>{0}", errorData.UserIdError);
            Console.WriteLine("passwordError==>{0}", errorData.PasswordError);
            await Expect(_loginPage._errorMessageUserId).ToBeVisibleAsync();
            await Expect(_loginPage._errorMessageUserId).ToHaveTextAsync(errorData.UserIdError);
            await Expect(_loginPage._errorMessagePassword).ToBeVisibleAsync();
            await Expect(_loginPage._errorMessagePassword).ToHaveTextAsync(errorData.PasswordError);
        }
         

    }
}
