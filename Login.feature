Feature: Google Search
  Verify that a search term returns relevant results

  Scenario: Search for Selenium C# NUnit on Google
    Given I am on the Google homepage
    When I search for "Selenium C# NUnit"
    Then the page title should contain "Selenium"