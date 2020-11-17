Feature: Remove an unnecessary task on my course to do list
  as student,I remove an unnecessary task from my course to do list, so
  I can forget about it.

  Background:
    Given I have a task "<task>"

  Scenario Outline: remove a task on my course to do list(Normal flow)
    Given task "<task>" doneStatus is true
    When I remove a task "<task>"
    Then the task "<task>" is removed
    And shut down server

    Examples:
      | task               |
      | scan paperwork     |


#  Scenario Outline: remove a non-existing task on my course to do list(Error flow)
#    Given I have a "<course>"
#    And the "<task>" does not exist
#    When I remove a "<task>" from "<course>"
#    Then I receive an error message
#
#    Examples:
#      | task         | course           |
#      | A2           | ECSE429          |
#      | P3           | ECSE424          |
#      | Midterm      | COMP350          |
#
#
#  Scenario Outline: remove multiple task on my course to do list, so I can forget it(Alternate flow)
#    Given I have a "<course>"
#    And I have multiple tasks "<taskList>"
#    When I remove "<taskList>" "<course>"
#    Then I can track my accomplishments "<course>"
#
#    Examples:
#      | taskList         | course           |
#      | A2,Midterm       | ECSE429          |
#      | P3,Reading       | ECSE424          |
#      | Midterm,A4       | COMP350          |