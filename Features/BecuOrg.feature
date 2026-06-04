Feature: BecuOrg

Verify UI features in home page of BECU.org

Background: 
	Given a user navigates to BECU home page

@smoke
Scenario: BECU logo refreshes the home page
	When the user clicks BECU logo
	Then BECU home page is refreshed with navbar displayed

@smoke
Scenario: EVERYDAY BANKING of dropdown menu displays savings shares
	When the user clicks EVERYDAY BANKING dropdown menu
	Then savings shares are displayed

@smoke
Scenario: Featured promotions slider is displayed
	When the user plays a button of slide on featured promotions slider
	Then the slides of featured promotions slider are displayed properly

@smoke
Scenario: Business Banking on secondary-promo launches Business Banking page
	When the user selects Business Banking on secondary-promo
	Then Business Banking page is launched

@smoke
Scenario: Contact on footer menu launches Contact page
	When the user selects Contact on footer menu
	Then Contact page is launched