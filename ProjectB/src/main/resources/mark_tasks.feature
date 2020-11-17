Feature: Mark a task on my course to do list
  as student,I mark a task as done on my course to do list, so
  I can track my accomplishments.

  Background:
    Given I have tasks registered in the system

  Scenario Outline: mark a task as done on course to do list, so I can tack accomplishments(Normal flow)
    Given I have a task "<task>"
    When I mark a task "<task>" as done
    Then task "<task>" is marked
    And shut down server

    Examples:
      | task             |
      | scan paperwork   |


  Scenario Outline: mark an invalid task as done (Error flow)
    Given I have a task "<task>"
    When I mark a task "<task>" as done
    Then I receive an error message
    And shut down server

    Examples:
      | task   |
      | what   |


  Scenario Outline: mark multiple multiple tasks as done(Alternate flow)
    Given I have multiple tasks "<taskList>"
    When I mark multiple tasks "<taskList>" as done
    Then tasks "<taskList>" is marked
    And shut down server

    Examples:
      | taskList                        |
      | scan paperwork,file paperwork   |