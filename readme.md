***************************
***************************
Automated UI Test Evaluation 
***************************
***************************

***************************
Frameworks / Tools used: 
***************************
- Nunit
- Selenium Chrome Driver

***************************
Tests Criteria / Requirements:
***************************
- Test 1: Order T-Shirt (and Verify in Order History) (Mandatory)
- Test 2: Update Personal Information (First Name) in My Account (Not mandatory, only if you have time.)

*********************
Notes for evaluator:
*********************
- Make sure the credentials to be used by the automated tests, are updated on file "testhost.dll.config"
-- accountEmail 
-- accountPassword
- Test execution result screenshot has been saved as "TestExecutionResults.png"

*********************
Potental Enhacements to Framework:
*********************
- Store strings that are repeteated and consistent on UI, on a Resource file
- Leverage those resource files for regionalization / localization
- Extend UtilHelper class to accomodate repeated string operations (or any other) 
- Include any pre-test browser settings on the SetupDriverWindow method (UtilHelper class)

 
--------------------------------------------------


***************************
***************************
Automated API Test Evaluation 
***************************
***************************

***************************
Frameworks / Tools used: 
***************************
- Nunit

***************************
Tests Criteria / Requirements:
***************************
1.	Create a new booking 
2.	Update a booking
3.	Delete a booking 

*********************
Notes for evaluator:
*********************
- Test execution result screenshot has been saved as "TestExecutionAPIResults.png"

*********************
Potental Enhacements to Framework:
*********************
- Refactor code
- Implement template method
- Implement Intefaces / contracts for CRUD operations
- Tests can be bundled to address edge case scenarios





