Feature: Change a task description
  As a student, I want to change a task description, to better represent the work to do.

  Background:
    Given I have description registered in the todoManagerRestAPI system

  Scenario Outline: change a task description to better represent work to do(Normal flow)
    Given I have tasks registered in the system
#    And I have a new description for a task
    When I change "<description>" of "<task>"
    Then the description of the "<task>" is changed to "<description>"
    And shut down server

    Examples:
      | task           		| description     			|
      | scan paperwork      | mandatory             	|
      | file paperwork      | mandatory 				|



  Scenario Outline: Change a description for a task which does not exist(Error flow)
    Given I have tasks registered in the system
#    And I want to change a description for a non-existing "<task>"
    When I change "<description>" of "<task>"
#    Then the "<taskDescription>" is not changed
    Then I receive an error message
    And shut down server
    Examples:
      | task           		| description			    |
      | task DNE        	| cover the whole semester	|



  Scenario Outline: change task descriptions to better represent work to do(Alternate flow)
    Given I have tasks registered in the system
#    And I have a new description for a task
    When I change "<description>" of multiple "<task>"
    Then the descriptions of the "<task>" are changed to "<description>"
    And shut down server

    Examples:
      | task           		| description     			|
      | scan paperwork, file paperwork      | mandatory, mandatory            	|


#
#  Scenario Outline: Change task descriptions of multiple tasks to better represent work to do(Alternate flow)
#    Given There are multiple "<taskList>" with "<taskDescriptionList>"
#    And I have multiple "newDescriptionList"
#    When I want to change the "<taskDescriptinoList>" to "<newDescriptionList>":
#      | taskList	             			 | newDescriptionList						|
#      | Final, Assignment         			 | cover the whole semester, chapters 5-6	|
#      | Project Deliverable, Research Paper  | due Nov 16th, due Nov 23rd				|
#
#    Then the "<taskDescriptionList>" are changed for "<taskList>":
#    Examples:
#      | taskList	             			 | taskDescriptionList						|
#      | Final, Assignment         			 | cover the whole semester, chapters 5-6	|
#      | Project Deliverable, Research Paper  | due Nov 16th, due Nov 23rd				|

