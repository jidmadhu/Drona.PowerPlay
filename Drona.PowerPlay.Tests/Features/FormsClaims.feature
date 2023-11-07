Feature: FormsClaims
	
@Dva
Scenario: Submit a Claim by Person
	Given The User navigate to WebSite
	And The User click on Advocacy under Find Help
	And The User click on DVA Claims
	When The User fills the DVA Claims Form
	And The User clicks the Submit button on the DVA Form
	Then The User can view "DVA Thank You"