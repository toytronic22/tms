@allure.epic:BDD @UI
Feature: Workouts
    As an athlete
    I want to keep my workouts in the calendar
    So that I can track my training

    Background:
        Given I am logged in

    @Smoke @critical
    Scenario: Add a run workout
        When I add a workout with the following data:
            | Field       | Value                |
            | Activity    | Run                  |
            | Name        | Morning run          |
            | Description | Easy run in the park |
            | Distance    | 5                    |
            | Unit        | km                   |
            | Duration    | 00:30:00             |
        Then the workout details show the entered data
        And the workout statistics are "5.00 km ~ 30:00"
        And the workout pace is "9:39 min/mi"
        And the workout is saved in the training log

    @Regression
    Scenario: Update workout name
        Given a run workout is created via API
        When I open the workout details
        And I change the workout name
        Then the workout details show the entered data
        And the workout is saved in the training log

    @Smoke @critical
    Scenario: Workout created via API is shown in the calendar
        Given a run workout is created via API
        When I open the calendar on the workout date
        Then the workout is displayed in the calendar

    @Regression
    Scenario: Delete workout from the calendar
        Given a run workout is created via API
        When I open the calendar on the workout date
        And I delete the workout from the calendar
        Then the workout is not displayed in the calendar
        And the workout is removed from the training log
