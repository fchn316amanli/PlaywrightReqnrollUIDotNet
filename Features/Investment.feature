Feature: Investment

Verify UI features in Investment Services page

Background:
	Given a user navigates to BECU home page
	When the user selects Investment Services under PLANNING&INVESTING dropdown menu

@regression
Scenario: Investment Services page can be launched
	Then Investment Services page is launched

@regression
Scenario: Leaving BECU website modal can be launched 
	When the user selects LPL Account View button
	Then Leaving BECU website modal is launched and verified

@regression
Scenario: MoneyGuide Pro video can be played
	When the user selects Play video button inside of MoneyGuide Pro section
	Then MoneyGuide Pro video is played


