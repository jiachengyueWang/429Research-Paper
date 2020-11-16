public class Task {

    private String id;
    private String title = " ";
    private String doneStatus = " ";
    private String description = " ";

    public Task (String id, String title, String doneStatus, String description){

        this.id = id;
        this.title = title;
        this.doneStatus = doneStatus;
        this.description = description;
    }

    public String getTitle(){
        return this.title;
    }

    public String getId(){
        return this.id;
    }

}
