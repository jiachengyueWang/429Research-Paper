using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentNDest : MonoBehaviour
{
    GameObject dest_in_use;
    GameObject agent_in_use;
    // Vector3 nextDestPosition;
    private Grid grid;
    private SetUp setup;
    // Start is called before the first frame update
    void Start()
    {
        grid = FindObjectOfType<Grid>();
        setup = FindObjectOfType<SetUp>();

        dest_in_use = setup.dest_in_use;
        agent_in_use = setup.agent_in_use;

    }

    // Update is called once per frame
    void Update()
    {
        if(grid.get_isDone()){
            Debug.Log("hey from pair");
            agent_in_use.transform.position = dest_in_use.transform.position + new Vector3 (0, 1f, 0);
            dest_in_use.transform.position = setup.nextDestPosition();
            

            grid.set_isDone(false);
        }
    }
}
