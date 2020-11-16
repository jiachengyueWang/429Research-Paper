import io.cucumber.datatable.DataTable;
import io.cucumber.java.After;
import io.cucumber.java.Before;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import kong.unirest.HttpResponse;
import kong.unirest.JsonNode;
import kong.unirest.Unirest;
import static org.junit.Assert.*;

import java.util.List;

public class stepDefinitions {

    int statusCode = 0;
    String errorMessage = "";
    int id = 1;

    @Then("shut down server")
    public static void shutDownServer(){
        Unirest.get("/shutdown");
    }

    @Given("I have categories registered in the todoManagerRestAPI system")
    public void i_have_categories_registered_in_the_todoManagerRestAPI_system() {
        HttpResponse<JsonNode> jsonResponse
                = Unirest.get("http://localhost:4567/categories")
                .asJson();
        assertNotNull(jsonResponse.getBody());
        assertEquals(200, jsonResponse.getStatus());
        }


    @Given("I have tasks registered in the system")
    public void i_have_tasks_registered_in_the_system() {
        HttpResponse<JsonNode> jsonResponse
                = Unirest.get("http://localhost:4567/todos")
                .asJson();
        assertNotNull(jsonResponse.getBody());
        assertEquals(200, jsonResponse.getStatus());
        }

    @When("I categorize {string} as {string}")
    public void i_categorize_task_as_priority(String t, String p) {
        id = 1;
        HttpResponse<JsonNode> jsonResponse = null;
        while(true) {
            jsonResponse = Unirest.get("http://localhost:4567/todos/{id}")
                    .routeParam("id", String.valueOf(id))
                    .asJson();
            if (jsonResponse.getBody().toString().contains(t)) {
                String rename = t + " " + p;
                HttpResponse<JsonNode> response = Unirest.post("http://localhost:4567/todos/" + id +"/categories")
                        .body("{\n\"title\":\"" + rename + "\"\n}\n")
                        .asJson();
                statusCode = response.getStatus();
                if(statusCode != 200 && statusCode != 201) {
                    errorMessage = response.getBody().getObject().getJSONArray("errorMessages").getString(0);
                }
                break;
            }
            else if(jsonResponse.getStatus() != 200){
                errorMessage = jsonResponse.getBody().getObject().getJSONArray("errorMessages").getString(0);
                break;
            }
            id++;
        }
    }

    @Then("the {string} should be categorized as {string}")
    public void the_should_be_categorized_as(String t, String p) {
        id = 1;
        String rename = t + " " + p;
        HttpResponse<JsonNode> jsonResponse = null;
        do {
            jsonResponse = Unirest.get("http://localhost:4567/todos/{id}")
                    .routeParam("id", String.valueOf(id))
                    .asJson();
            if (jsonResponse.getBody().toString().contains(rename)) {
                break;
            }
            id++;
        }while (jsonResponse.getStatus() == 200);
    }

    @Then("I receive an error message")
    public void i_receive_an_error_message() {
        System.out.println(errorMessage);
    }

    @When("I categorize multiple tasks {string} as {string}")
    public void i_categorize_multiple_tasks_as(String tl, String p) {
        String[] tasks = tl.split(",");
        for(String i : tasks){
            i_categorize_task_as_priority(i, p);
        }
    }

    @Then("the tasks {string} should be categorized as {string}")
    public void the_tasks_should_be_categorized_as(String tl, String p) {
        String[] tasks = tl.split(",");
        for(String i : tasks){
            the_should_be_categorized_as(i, p);
        }
    }


}
