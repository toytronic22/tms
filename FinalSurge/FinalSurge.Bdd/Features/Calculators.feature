@allure.epic:BDD @UI
Feature: Calculators
    As an athlete
    I want to use the training calculators
    So that I can plan my nutrition and race pace

    Background:
        Given I am logged in
        And I open the other calculators

    @Smoke
    Scenario Outline: Calculate daily caloric needs
        When I enter weight "<weight>" in "<weight unit>"
        And I enter height "<height>" in "<height unit>"
        And I enter age "<age>" and gender "<gender>"
        And I enter run distance "<distance>" in "<distance unit>"
        And I calculate caloric needs
        Then the non-activity calories are "<non-activity>"
        And the training calories are "<training>"
        And the total calories are "<total>"

        Examples:
            | weight | weight unit | height | height unit | age | gender | distance | distance unit | non-activity | training | total      |
            | 70     | kg          | 180    | cm          | 30  | male   | 10       | km            | 2,018 kCal   | 700 kCal | 2,718 kCal |
            | 150    | lbs         | 65     | in          | 25  | female | 5        | mi            | 1,713 kCal   | 547 kCal | 2,261 kCal |

    @Regression
    Scenario: Calculate pace for a custom distance
        When I open the pace calculator
        And I calculate pace for "10" "Kilometers" with time "00:50:00"
        Then the pace per kilometer is "5:00 min/km"
        And the pace per mile is "8:02 min/mi"

    @Regression
    Scenario Outline: Calculate pace for a race distance
        When I open the pace calculator
        And I calculate pace for race "<race>" with time "<time>"
        Then the pace per kilometer is "<pace per km>"
        And the pace per mile is "<pace per mile>"

        Examples:
            | race     | time     | pace per km | pace per mile |
            | Marathon | 03:30:00 | 4:58 min/km | 8:00 min/mi   |
