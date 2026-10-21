@allure.epic:BDD @UI
Feature: Login
    As an athlete
    I want to log in to Final Surge
    So that I can see my training log

    Background:
        Given I am on the login page

    @Smoke @blocker
    Scenario: Login with valid credentials
        When I log in with valid credentials
        Then the dashboard is displayed
        And my first name is shown in the header

    @Regression
    Scenario: Login with wrong password
        When I log in with email "nobody.tms@example.com" and password "wrong-password"
        Then the error message "Invalid login credentials. Please try again." is shown

    @Regression
    Scenario Outline: Email validation on the login form
        When I log in with email "<email>" and password "wrong-password"
        Then the email validation message "<message>" is shown

        Examples:
            | email      | message                             |
            |            | Please enter your e-mail address.   |
            | nobody.tms | Please enter a valid email address. |

    @Regression
    Scenario: Password validation on the login form
        When I log in with email "nobody.tms@example.com" and password ""
        Then the password validation message "Please enter a password." is shown

    @Smoke
    Scenario: Logout
        When I log in with valid credentials
        And I log out
        Then the login page is displayed
