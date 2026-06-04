Feature: Login

Verify UI features in Login page

Background:
	Given a user navigates to BECU home page
	When the user clicks Login button on the top right corner of home page

@smoke
Scenario: Login page is launched and verified
	Then Login page is launched and verified

@regression
Scenario: Fake user ID and password get error message
	When the user inputs the following fake username and password
			| Username	  | Password   |
			| dummytest   | dummytest  |
	Then error message is displayed
	"""
	The User ID and/or Password you provided does not match our records. Please re-enter your information and try again.
	"""

@regression
Scenario: Blank user ID and password get error messages
	When the user inputs blank username and password
	Then user ID and password error messages are displayed as below
		| UserIdError				| PasswordError				 |
		| Please enter your user ID.| Please enter your password.|