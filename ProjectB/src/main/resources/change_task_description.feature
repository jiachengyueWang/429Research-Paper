Feature: Change a task description
  As a student, I want to change a task description, to better represent the work to do.

  Background:
    Given I am a "student"

  Scenario Outline: change a task description to better represent work to do(Normal flow)
    Given I have tasks with title, doneStatus and description registered in the system
#      | task           | doneStatus | description |
      | scan paperwork | false |      |
      | file paperwork | true  |      |

    And I have a new description for the "<task>"
    When I change the "<description>" to the new description:
#      | task           		| newDescription			|
#      | Final         		| cover the whole semester	|
#      | Assignment      	| chapter 1 - 3				|
#      | Project Deliverable | deliverable  B			|

    Then the "<description>" is changed:
    Examples:
      | task           		| description     			|
      | scan paperwork      | due on nov 16th       	|
      | file paperwork      | chapter 1 - 3				|



  Scenario Outline: Change a task description to better represent work to do(Error flow)
    Given I have tasks with title, doneStatus and description registered in the system
      | scan paperwork | false |      |
      | file paperwork | true  |      |
    And I want to change a description for a task
    But the "<task>" does not exist
    When I want to change the "<taskDescription>" to "<newDescription>":
#      | task           		| newDescription			|
#      | Final         		| cover the whole semester	|
#      | Assignment      	| chapter 1 - 3				|
#      | Quiz				| chapter 2 - 4				|

    Then the "<taskDescription>" is not changed
    And I receive an error message:
    Examples:
      | task           		| taskDescription			|
      | Final         		| cover the whole semester	|
      | Assignment      	| chapter 1 - 3				|
      | Task DNE			| Task DNE					|


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

