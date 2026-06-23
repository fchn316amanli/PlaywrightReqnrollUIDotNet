Feature: Investment

Verify UI features in Investment pages

Background:
	Given a user navigates to BECU home page

@regression
Scenario: Investment Services page can be launched
	When the user selects Investment Services under PLANNING&INVESTING dropdown menu
	Then Investment Services page is launched

@regression
@iFrameTest
@IFramedVideo
Scenario: Leaving BECU website modal can be launched
	When the user selects Investment Services under PLANNING&INVESTING dropdown menu
	When the user selects LPL Account View button
	Then Leaving BECU website modal is launched and verified

@regression
@iFrameTest
@IFramedVideo
Scenario: MoneyGuide Pro video can be played
	When the user selects Investment Services under PLANNING&INVESTING dropdown menu
	When the user selects Play video button inside of MoneyGuide Pro section
	Then MoneyGuide Pro video is played

@regression
Scenario: Investment tools and calculators page can be launched
	When the user selects Investment Calculators under PLANNING&INVESTING dropdown menu
	Then Investment tools and calculators page is launched

@regression
@iFrameTest
@IFramedBarChart
Scenario: bar chart of Which will provide the most retirment income can be displayed
	When the user selects Investment Calculators under PLANNING&INVESTING dropdown menu
	When the user scrolls down to the Calculator of Which will provide the most retirment income and clicks
	Then the bar chart of Which will provide the most retirment income is displayed

@regression
@iFrameTest
@IFramedBarChart
Scenario: Tooltip of bar chart can be displayed
	When the user selects Investment Calculators under PLANNING&INVESTING dropdown menu
	When the user scrolls down to the Calculator of Which will provide the most retirment income and clicks
	When the user hovers the first bar of the bar chart
	Then the tooltip of bar chart is displayed

@regression
@iFrameTest
@IFramedBarChart
Scenario: Bars of bar chart can be deselected and hidden
	When the user selects Investment Calculators under PLANNING&INVESTING dropdown menu
	When the user scrolls down to the Calculator of Which will provide the most retirment income and clicks
	When the user deselects the first bar of the bar chart
	Then the bar of bar chart is hidden

@regression
@iFrameTest
@IFramedTable
Scenario: Details Table of bar chart can be displayed
	When the user selects Investment Calculators under PLANNING&INVESTING dropdown menu
	When the user scrolls down to the Calculator of Which will provide the most retirment income and clicks
	When the user selects the button of Show Details
	Then the Details Table of bar chart is displayed

@regression
@iFrameTest
@IFramedTable
Scenario: Data Table of bar chart can be displayed
	When the user selects Investment Calculators under PLANNING&INVESTING dropdown menu
	When the user scrolls down to the Calculator of Which will provide the most retirment income and clicks
	When the user selects the button of View as data table
	Then the Data Table of bar chart is displayed

@regression
@iFrameTest
Scenario: Input of bar chart can be changed
	When the user selects Investment Calculators under PLANNING&INVESTING dropdown menu
	When the user scrolls down to the Calculator of Which will provide the most retirment income and clicks
	When the user changes the inputs of bar chart
	Then the result of bar chart is updated