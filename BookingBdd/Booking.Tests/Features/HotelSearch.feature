Feature: Hotel search
  As a traveller
  I want to find a hotel by its name
  So that I can check its rating

  Scenario Outline: Searched hotel is shown with expected rating
    Given the search results page is open
    When I search for "<hotel>"
    Then the hotel "<hotel>" is shown on the page
    And the hotel "<hotel>" has rating "<rating>"

    Examples:
      | hotel                        | rating |
      | Hilton Garden Inn Tirana     | 9.0    |
      | Hotel Adlon Kempinski Berlin | 9.1    |
