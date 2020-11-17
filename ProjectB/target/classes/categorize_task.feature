Feature: Categorize tasks to better manage my time
  as a student,I categorize tasks as HIGH, MEDIUM or LOW priority
  so I can better manage my time

  Background:
    Given I have categories registered in the todoManagerRestAPI system

  Scenario Outline: Categorize tasks and it helps manage my time(Normal flow)
    Given I have tasks registered in the system
    When I categorize "<title>" as "<priority>"
    Then the "<title>" should be categorized as "<priority>"
    And shut down server

    Examples:
      | priority    | title             |
      | HIGH        | scan paperwork    |
      | LOW         | file paperwork    |


  Scenario Outline: Categorize tasks by priority, but the task does not exist(Error flow)
    Given I have tasks registered in the system
    When I categorize "<title>" as "<priority>"
    Then I receive an error message
    And shut down server

    Examples:
      | title       | priority   |
      | what        | LOW        |


  Scenario Outline:  Categorize multiple tasks by priority(Alternate flow)
    Given I have tasks registered in the system
    When I categorize multiple tasks "<titleList>" as "<priority>"
    Then the tasks "<titleList>" should be categorized as "<priority>"
    And shut down server

    Examples:
      | titleList                          | priority  |
      | scan paperwork,file paperwork      | HIGH      |

