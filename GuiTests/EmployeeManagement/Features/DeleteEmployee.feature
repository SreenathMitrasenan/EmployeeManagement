Feature: DeleteEmployee

As a user I want to delete an existing employee record

@smoketest
Scenario: 01_Delete employee in employee manager appliation and verify its deleted
	Given I lauch application
	Then I click on newEmployee webelement present in Home page
	Then I set below values in CreateEmployee page
	| key            | value               |
	| name           | Tom                 |
	| email          | tomcruise@gmail.com |
	| male           | true                |
	| active         | true                |
	| department     | Foxtrot             |
	| proofSubmitted | PAN                 |
	| salary         | 175000              |
	Then I click on save webelement present in CreateEmployee page
	Then I verify and accept alert message with text Data Inserted Successfully! on CreateEmployee page
	Then I click on allUsers webelement present in CreateEmployee page
	Then I delete the employee having NAME property as Tom on Home page
	Then I verify and accept alert message with text Do you really want to delete this record? on Home page
	Then I verify and accept alert message with text Data Deleted Successfully! on Home page
	

	
