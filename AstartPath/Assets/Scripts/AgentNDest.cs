using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNDest : MonoBehaviour
{
    
    GameObject agent_in_use;
    GameObject dest_in_use;
    // int agent_id ;
    // int dest_id ;
    Vector3 agent_position;
    Vector3 dest_position;



    GameObject[] wayPoints;
    public float speed = 2;
    public bool rand = false;
    public bool go = true;

    private Grid grid;
    private SetUp setup;
    bool isDone = false;
    // Start is called before the first frame update
    void Start()
    {

        grid = FindObjectOfType<Grid>();
        // setup = FindObjectOfType<SetUp>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(ArrayList path, GameObject ai){
        foreach(Cell cell in path){
            ai.transform.LookAt(cell.transform.position);
            // ai.transform.position += ai.transform.forward * Time.deltaTime;
            ai.transform.position = cell.transform.position;
            // ai.transform.position = Vector3.MoveTowards(transform.position, cell.transform.position, Time.deltaTime * 1.2f);
            Debug.Log(ai.transform.position);
            // agent_in_use.transform.LookAt(cell.transform.position);
            // agent_in_use.transform.position += agent_in_use.transform.forward * speed * Time.deltaTime;
        }   
    }


    public AgentNDest(GameObject agent, GameObject dest, Vector3 agent_position, Vector3 dest_position){
        this.agent_in_use = agent;
        this.dest_in_use = dest;
        this.agent_position = agent_position;
        this.dest_position = dest_position;
    }


    public Vector3 getAgentPosition(){
        return agent_position;
    }

    public Vector3 getDestPosition(){
        return dest_position;
    }
    
    public GameObject getAgent(){
        return agent_in_use;
    }

    public GameObject getDest(){
        return dest_in_use;
    }

    public bool get_isDone(){
        return isDone;
    }

    public void set_isDone(bool isDone){
        this.isDone = isDone;
    }

}














// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AgentNDest : MonoBehaviour
// {
//     GameObject dest_in_use;
//     GameObject agent_in_use;
//     // Vector3 nextDestPosition;
//     private Grid grid;
//     private SetUp setup;
//     // Start is called before the first frame update
//     void Start()
//     {
//         grid = FindObjectOfType<Grid>();
//         setup = FindObjectOfType<SetUp>();

//         dest_in_use = setup.dest_in_use;
//         agent_in_use = setup.agent_in_use;

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // if(grid.get_isDone()){
//         //     Debug.Log("hey from pair");
//         //     agent_in_use.transform.position = dest_in_use.transform.position + new Vector3 (0, 1f, 0);
//         //     dest_in_use.transform.position = setup.nextDestPosition();
            

//         //     grid.set_isDone(false);
//         // }
//     }
// }