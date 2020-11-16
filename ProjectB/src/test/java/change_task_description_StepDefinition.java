import io.cucumber.datatable.DataTable;
import io.cucumber.java.After;
import io.cucumber.java.Before;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import kong.unirest.HttpResponse;
import kong.unirest.JsonNode;
import kong.unirest.Unirest;

import java.util.List;

public class change_task_description_StepDefinition {
    int statusCode = 0;
    String errorMessage = "";

    @After
    public static void shutDownServer(){
        Unirest.get("/shutdown");
    }

    @Given("I have categories registered in the todoManagerRestAPI system")
    public void i_have_categories_registered_in_the_todoManagerRestAPI_system(DataTable table) {
        List<List<String>> rows = table.asLists(String.class);
        for (List<String> columns : rows) {
            Unirest.post("http://localhost:4567/categories")
                    .body("{\n\"description\":\"" + columns.get(1) + "\",\n  \"title\":\""
                            + columns.get(0) + "\"\n}")
                    .asJson();
        }
    }

    @Given("I have tasks with title, doneStatus and description registered in the system")
    public void i_have_tasks_with_title_doneStatus_and_description_registered_in_the_system(DataTable table) {
        List<List<String>> rows = table.asLists(String.class);
        for (List<String> columns : rows) {
            Unirest.post("http://localhost:4567/todos")
                    .body("{\"title\":\"" + columns.get(0) + "\",\"doneStatus\":"
                            + columns.get(1) + ",\"description\":\"" + columns.get(2) + "\"}")
                    .asJson();
        }
    }
}
