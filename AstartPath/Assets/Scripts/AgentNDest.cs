using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNDest : MonoBehaviour
{

    GameObject agent_in_use;
    GameObject dest_in_use;
    int agent_id ;
    int dest_id ;

    private Grid grid;
    private SetUp setup;
    // Start is called before the first frame update
    void Start()
    {

        grid = FindObjectOfType<Grid>();
        // setup = FindObjectOfType<SetUp>();

    }

    // Update is called once per frame
    void Update()
    {
        // if(!grid.isCalculating){
        //     if (Input.GetKey(KeyCode.Mouse0)){
        //         Debug.Log(agent_id);
        //         Debug.Log(dest_id);
        //         // grid.CalculatePathExternal(agent_id, dest_id);
        //     }
        // }
        // if (!grid.isCalculating) {
        //     if (Input.GetKey(KeyCode.Mouse0)){
        //         for(int i = 0; i < numPair; i++){
        //             Debug.Log("Pair");
                    
                    
        //         }
        //         grid.CalculatePathExternal(agent_id, dest_id);
        //     }
            

        // }
        // if(grid.get_isDone()){
            
        //     //StartCoroutine( update_Pair());
        //     //remove the positions
        //     setup.removeLocation(agent_in_use.transform.position);
        //     agent_in_use.transform.position = dest_in_use.transform.position + new Vector3 (0, 1f, 0);
        //     setup.removeLocation(dest_in_use.transform.position);
        //     dest_in_use.transform.position = setup.nextDestPosition();

        //     grid.set_isDone(false);
        // }
    }


    public AgentNDest(GameObject agent, GameObject dest, int agent_id, int dest_id){
        this.agent_in_use = agent;
        this.dest_in_use = dest;
        this.agent_id = agent_id;
        this.dest_id = dest_id;
        // startPathing();
    }

    void startPathing(){
        grid.CalculatePathExternal(agent_id, dest_id);
    }

    // public void createPair(Vector3 agent_position, Vector3 dest_position){
    //     int prefab_id = Random.Range(0, 6);
    //     Instantiate(agents[prefab_id], agent_position + new Vector3 (0, 1, 0), Quaternion.identity);
    //     Instantiate(destinations[prefab_id], dest_position, Quaternion.identity);
    // }

    // public void setID(int agent_id, int dest_id){
        
    //     this.agent_id = agent_id;
    //     this.dest_id = dest_id;
    // }

    // //just checking
    // public void printID(){
    //     Debug.Log(agent_id);
    //     Debug.Log(dest_id);
    // }





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