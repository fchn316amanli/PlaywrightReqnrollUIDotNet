using Microsoft.Playwright;
using Microsoft.VisualBasic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace PlaywrightReqnrollUIDotNet.Pages
{
    internal class LoginPage
    {
        private readonly IPage _page;
        public ILocator _pageHeading1 => _page.GetByRole(AriaRole.Heading, new() { Name = "BECU Log In Options" });
        public ILocator _pageHeading2 => _page.GetByRole(AriaRole.Heading, new() { Name = "Online Banking", Exact = true });

        public ILocator _userIdLabel => _page.GetByLabel("User ID");
        public ILocator _usernameInput => _page.Locator("input[name='username']");
        public ILocator _passwordLabel => _page.Locator("#login-form").GetByText("Password", new() { Exact = true });
        public ILocator _passwordInput => _page.Locator("input[name='password']");
        public ILocator _loginBtn => _page.GetByRole(AriaRole.Button, new() { Name = "Log In" });

        public ILocator _forgetPasswordLnk => _page.GetByRole(AriaRole.Link, new() { Name = "Forgot your Password?" });
        public ILocator _forgetUserIdLnk => _page.GetByRole(AriaRole.Link, new() { Name = "Forgot your User ID?" });
        public ILocator _alreadyMemberLabel => _page.GetByText("Already a Member?");
        public ILocator _notEnrollYetText => _page.GetByText("If you are not enrolled in Online & Mobile Banking yet");
        public ILocator _toEnrollLnk => _page.GetByRole(AriaRole.Link, new() { Name = "click here to enroll" });
        public ILocator _notMemberLabel => _page.GetByText("Not a Member?");
        public ILocator _signUpMembershipLnk => _page.GetByRole(AriaRole.Link, new() { Name = "Sign up for membership with BECU" });


        public ILocator _pageHeading2Biz => _page.GetByRole(AriaRole.Heading, new() { Name = "Business Online Banking" });
        public ILocator _forLLCText => _page.GetByText("For LLCs using an EIN");
        public ILocator _bizOnlineBankingBtn => _page.GetByRole(AriaRole.Link, new() { Name = "Business Online Banking", Exact = true });
        public ILocator _enrollBizOnlineBankingBtn => _page.GetByRole(AriaRole.Link, new() { Name = "Enroll in Business Online Banking" });

        //public ILocator _errorMessage => _page.Locator(".error-message-container").First;
        public ILocator _errorMessage => _page.Locator("#error-message");
        public ILocator _errorMessageUserId => _page.Locator("#userId-error");
        public ILocator _errorMessagePassword => _page.Locator("#password-error");


        public LoginPage(IPage page) => _page = page;

        //To-do
        public async Task ClickLoginBtn() => await _loginBtn.ClickAsync();

        //To-do
        public async Task LoginInput(string username, string password)
        {
            /*
            await _usernameInput.FillAsync(username);
            await _passwordInput.FillAsync(password);
            await _signInBtn.ClickAsync();
            */
        }

    }
}
