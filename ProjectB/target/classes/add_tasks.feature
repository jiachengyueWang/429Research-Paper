Feature: Add tasks to course to do list
  as student,I add a task to a course to do list, so
  I can remember it.

  Background:
    Given I have a task to be registered in the todoManagerRestAPI system
      | A3 | false |       |

  Scenario Outline: Add a task to a to do list(Normal flow)
    When I add task "<title>", "<doneStatus>", "<description>"
    Then the task should be registered "<title>"
    And shut down server

    Examples:
      | title  | doneStatus    | description |
      | A3     | false         |             |


  Scenario Outline: Add a task to a to do list, but the task is invalid(Error flow)
    When I add task "<title>", "<doneStatus>", "<description>"
    Then I receive an error message
    And shut down server

    Examples:
      | title  | doneStatus    | description |
      | A3     | happy         |             |

  Scenario Outline:  Add multiple tasks to a to do list(Alternate flow)
    When I add multiple tasks "<titleList>", "<doneStatusList>", "<descriptionList>"
    Then the tasks should be registered "<titleList>"
    And shut down server

    Examples:
      | titleList     | doneStatusList        | descriptionList |
      | A4,A5         | false, false          | none,none       |

