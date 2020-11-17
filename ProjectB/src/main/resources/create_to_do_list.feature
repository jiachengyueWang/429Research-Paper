Feature: Create a to do list for a new class
  as student, I create a to do list for a new class I am taking, so
  I can manage course work.

  Background:
    Given I have a new project "<project>"
    |research paper, false, true,  |

  Scenario Outline: create a to do list for a class(Normal flow)
    Given I have a to do list <to do list>
    When I create a to do list for the project "<project>"
    Then shut down server

    Examples:
      | project                       | to do list |
      | research paper,false,true,    | 1,2        |


#  Scenario Outline: create a to do list for a class, but registered course list is full(Error flow)
#    Given There is a new "<course>"
#    And max number of registered courses per term is reached
#    When I create a to do list for the "<course>"
#    Then I receive an error message
#
#    Examples:
#      | course           |
#      | ECSE429          |
#      | ECSE424          |
#      | COMP350          |
#
#
#  Scenario Outline: create multiple to do lists for multiple classes, so I can manage course work(Alternate flow)
#    Given There are multiple "<courseList>"
#    And max number of registered courses per term is not reached
#    When I create to do lists for the "<courseList>"
#    Then I can manage my course work
#
#    Examples:
#      | courseList               |
#      | ECSE429,COMP202          |
#      | ECSE424,COMP302,COMP303  |
#      | COMP350,COMP361          |