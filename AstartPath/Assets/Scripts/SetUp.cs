using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SetUp : MonoBehaviour
{

    public GameObject obstacle_block;
    public int numObstacles;

    //agents
    public GameObject agent;
    public GameObject agent_0;
    public GameObject agent_1;
    public GameObject agent_2;
    public GameObject agent_3;
    public GameObject agent_4;
    public GameObject agent_5;
    public GameObject agent_6;
    public int numAgent;

    //destinations
    public GameObject dest;
    public GameObject dest_0;
    public GameObject dest_1;
    public GameObject dest_2;
    public GameObject dest_3;
    public GameObject dest_4;
    public GameObject dest_5;
    public GameObject dest_6;
    


    //position of the grid
    public float xPos;
    public float zPos;

    //store the locations of all the obstacle blocks generated
    List<Vector3> locations = new List<Vector3>();
    List<GameObject> agents = new List<GameObject>();
    List<GameObject> destinations = new List<GameObject>();


    // Start is called before the first frame update



    void Start()
    {
        //spawn Obstacles
        int spawned = 0;
        //Generate the first obstacle
        Vector3 position = new Vector3(Random.Range(xPos, xPos + 20f), 0.3f, Random.Range(zPos, zPos + 15f));
        Vector3 last = position;
        Instantiate(obstacle_block, position, Quaternion.identity);
        Debug.Log("first obstacle center" + position);
        locations.Add(position);
        generateObstacle(position);
        spawned++;

        Debug.Log("1 obstacle DONE");

        //spawn another 3 obstacles
		while (spawned < numObstacles) {
            //generate a random location
            position = new Vector3(Random.Range(xPos, xPos + 20f), 0.3f, Random.Range(zPos, zPos + 15f));
            Debug.Log("first recommend " + position);

            //check if this location overlaps with others, if it does, keep updating the location
            while (overlap(position))
            {
                position = new Vector3(Random.Range(xPos, xPos + 20f), 0.3f, Random.Range(zPos, zPos + 15f));
            }
            Debug.Log(position);
            // Debug.Log(inRadius(last, position));
            Instantiate(obstacle_block, position, Quaternion.identity);
            locations.Add(position);
            generateObstacle(position);
            spawned++;
            Debug.Log( spawned + " obstacle DONE");
        }


        //add Agents prefab
        agents.Add(agent);
        agents.Add(agent_0);
        agents.Add(agent_1);
        agents.Add(agent_2);
        agents.Add(agent_3);
        agents.Add(agent_4);
        agents.Add(agent_5);
        agents.Add(agent_6);

        //add destination prefab
        destinations.Add(dest);
        destinations.Add(dest_0);
        destinations.Add(dest_1);
        destinations.Add(dest_2);
        destinations.Add(dest_3);
        destinations.Add(dest_4);
        destinations.Add(dest_5);
        destinations.Add(dest_6);

        generateAgents();


		// //test for locations
		// for(int i = 0; i < locations.Count; i++){
        //    Debug.Log(locations[i]);
		// }



    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void generateObstacle(Vector3 center){

        Vector3 loc = new Vector3(0, 0, 0);
        //even:up, odd:down
        int vertical = Random.Range(0, 100);

		if (vertical % 2 == 0 || center.z <= 2) {
            loc = new Vector3(center.x, center.y, center.z + 1);
            Instantiate(obstacle_block, loc, Quaternion.identity);
            Debug.Log("lateral obstacle" + loc);
            locations.Add(loc);
            int horizontal = Random.Range(0, 100);
            if (horizontal % 2 == 0 || center.x <= 3)
            {
                loc = new Vector3(center.x + 1, center.y, center.z);
                Instantiate(obstacle_block, loc, Quaternion.identity);
                locations.Add(loc);
                Debug.Log("lateral obstacle" + loc);
                loc = new Vector3(center.x + 2, center.y, center.z);
                Instantiate(obstacle_block, loc, Quaternion.identity);
                locations.Add(loc);
                Debug.Log("lateral obstacle" + loc);
            }
            else if(horizontal % 2 == 1 || center.x >= 17)
            {
                loc = new Vector3(center.x - 1, center.y, center.z);
                Instantiate(obstacle_block, loc, Quaternion.identity);
                locations.Add(loc);
                Debug.Log("lateral obstacle" + loc);
                loc = new Vector3(center.x - 2, center.y, center.z);
                Instantiate(obstacle_block, loc, Quaternion.identity);
                locations.Add(loc);
                Debug.Log("lateral obstacle" + loc);
            }

        }
        else if (vertical % 2 == 1 || center.z >= 12) {
            loc = new Vector3(center.x, center.y, center.z - 1);
            Instantiate(obstacle_block, loc, Quaternion.identity);
            locations.Add(loc);
            Debug.Log("lateral obstacle " + loc);
            int horizontal = Random.Range(0, 1);
			if (horizontal % 2 == 0 || center.x <= 3)
            {
                loc = new Vector3(center.x + 1, center.y, center.z);
                Instantiate(obstacle_block, loc, Quaternion.identity);
                locations.Add(loc);
                Debug.Log("lateral obstacle " + loc);
            }
            else if(horizontal % 2 == 1 || center.x >= 17)
            {
                loc = new Vector3(center.x - 1, center.y, center.z);
                Instantiate(obstacle_block, loc, Quaternion.identity);
                locations.Add(loc);
                Debug.Log("lateral obstacle " + loc);
            }
        }
    }


    void generateAgents()
    {
        int spawned = 0;

		while (spawned < numAgent)
		{
            Vector3 position_start = new Vector3(Random.Range(xPos, xPos + 20f), 1f, Random.Range(zPos, zPos + 15f));
            Vector3 position_dest = new Vector3(Random.Range(xPos, xPos + 20f), 0f, Random.Range(zPos, zPos + 15f));
            //spawn the agent
            while (overlap(position_start))
            {
                position_start = new Vector3(Random.Range(xPos, xPos + 20f), 1f, Random.Range(zPos, zPos + 15f));
            }
            Instantiate(agents[spawned % numAgent], position_start, Quaternion.identity);
            locations.Add(position_start);
            Debug.Log("agent " + position_start);

            //spawn the destination of the agent
            while (overlap(position_dest))
            {
                position_dest = new Vector3(Random.Range(xPos, xPos + 20f), 0f, Random.Range(zPos, zPos + 15f));
            }
            Instantiate(destinations[spawned % numAgent], position_dest, Quaternion.identity);
            locations.Add(position_dest);
            Debug.Log("agent " + position_dest);
            
            spawned++;
        }
        


    }

    //check if we chose a valid position to spawn objects
    bool overlap(Vector3 loc){
        bool overlap = false;
		for (int i = 0; i < locations.Count; i++) {
            //if distance between them < 3, then not a valid location -> returns true
			if (Math.Abs(loc.x - locations[i].x) < 4 && Math.Abs(loc.z - locations[i].z) < 4) { 
                Debug.Log("distance x" + Math.Abs(loc.x - locations[i].x) + "distance y " + Math.Abs(loc.z - locations[i].z));
                return true;
            }
        }
        Debug.Log(overlap);
        return overlap;
    }


    // bool validLoc(Vector3 loc)
	// {
    //     bool overlap = false;
	// 	for (int i = 0; i < locations.Count; i++) {
	// 		if (Math.Abs(loc.x - locations[i].x) < 2.5) { overlap = true; }
    //         if (Math.Abs(loc.z - locations[i].z) < 2.5) { overlap = true; }
    //     }
    //     return overlap;

	// }


    // bool inRadius(Vector3 last, Vector3 now) {
    //     //radius = 2

    //     bool inRadius = false;

	// 	if (Math.Abs(last.x - now.x) <= 3 && Math.Abs(last.z - now.z) <= 3)
	// 	{
    //         inRadius = true;
	// 	}
    //     return inRadius;
    // }




}
