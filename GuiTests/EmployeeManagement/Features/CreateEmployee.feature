Feature: CreateEmployee

As a user I want to create employee using basic details
@smoketest
Scenario: 01_Create employee in employee manager appliation and verify its created and delete the same
	Given I lauch application
	Then I click on newEmployee webelement present in Home page
	Then I set below values in CreateEmployee page
	| key            | value               |
	| name           | Roger               |
	| email          | roger1885@gmail.com |	
	| proofSubmitted | PAN                 |	
	| salary         | 175000              |
	Then I click on save webelement present in CreateEmployee page
	Then I verify and accept alert message with text Data Inserted Successfully! on CreateEmployee page
	Then I click on allUsers webelement present in CreateEmployee page	
	Then I delete the employee having NAME property as Roger on Home page
	Then I verify and accept alert message with text Do you really want to delete this record? on Home page
	Then I verify and accept alert message with text Data Deleted Successfully! on Home page	

@smoketest
	Scenario: 02_Create employee in employee manager appliation and verify its created and delete the same
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

@basic
	Scenario: 03_Create an employee record with exisitng emailid in employee manager application
	Given I lauch application
	Then I click on newEmployee webelement present in Home page
	Then I set below values in CreateEmployee page
	| key            | value                |
	| name           | Hrom                 |
	| email          | tromcruise@gmail.com |
	| male           | true                 |
	| active         | true                 |
	| department     | Foxtrot              |
	| proofSubmitted | PAN                  |
	| salary         | 175000               |
	Then I click on save webelement present in CreateEmployee page
	Then I verify and accept alert message with text Data Inserted Successfully! on CreateEmployee page
	Then I click on allUsers webelement present in CreateEmployee page

