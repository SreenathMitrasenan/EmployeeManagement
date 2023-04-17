Feature: SwagLabs

As a user I want to login different swaglab users 

@swag
Scenario: 01_Login swag lab using standard user and logout
	Given I lauch swag application
	Then I set below values in SwagLabLogin page
	| key      | value         |
	| userName | standard_user |
	| password | secret_sauce  |
	Then I click on login webelement present in SwagLabLogin page
	Then I click on burgerButton webelement present in SwagLabHome page
	Then I click on logout webelement present in SwagLabHome page
	
@swag
Scenario: 02_Login swag lab using lockedout user and logout
	Given I lauch swag application
	Then I set below values in SwagLabLogin page
	| key      | value           |
	| userName | locked_out_user |
	| password | secret_sauce    |
	Then I click on login webelement present in SwagLabLogin page
	Then I click on burgerButton webelement present in SwagLabHome page
	Then I click on logout webelement present in SwagLabHome page

@swag
Scenario: 03_Login swag lab using problem user and logout
	Given I lauch swag application
	Then I set below values in SwagLabLogin page
	| key      | value        |
	| userName | problem_user |
	| password | secret_sauce |
	Then I click on login webelement present in SwagLabLogin page
	Then I click on burgerButton webelement present in SwagLabHome page
	Then I click on logout webelement present in SwagLabHome page

@swag
Scenario: 04_Login swag lab using standard user and logout
	Given I lauch swag application
	Then I set below values in SwagLabLogin page
	| key      | value         |
	| userName | standard_user |
	| password | secret_sauce  |
	Then I click on login webelement present in SwagLabLogin page
	Then I click on burgerButton webelement present in SwagLabHome page
	Then I click on logout webelement present in SwagLabHome page


@swag
Scenario: 05_Login swag lab using performance glitch user and logout
	Given I lauch swag application
	Then I set below values in SwagLabLogin page
	| key      | value                   |
	| userName | performance_glitch_user |
	| password | secret_sauce            |
	Then I click on login webelement present in SwagLabLogin page
	Then I click on burgerButton webelement present in SwagLabHome page
	Then I click on logout webelement present in SwagLabHome page

	
