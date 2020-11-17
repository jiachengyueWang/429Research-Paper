using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SetUp : MonoBehaviour
{

    public GameObject obstacle_block;

    public GameObject agent;
    public GameObject agent_0;
    public GameObject agent_1;
    public GameObject agent_2;
    public GameObject agent_3;
    public GameObject agent_4;
    public GameObject agent_5;
    public GameObject agent_6;
    public int numAgent;

    //position of the grid
    public float xPos;
    public float zPos;

    List<Vector3> locations = new List<Vector3>();
    List<GameObject> agents = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //spawn Obstacles
        int spawned = 0;
        //Generate the first obstacle
        Vector3 position = new Vector3(Random.Range(xPos, xPos + 20f), 0.3f, Random.Range(zPos, zPos + 15f));
        Vector3 last = position;
        Instantiate(obstacle_block, position, Quaternion.identity);
        Debug.Log(position);
        locations.Add(position);
        generateObstacle(position);
        spawned++;
        //spawn another 3 obstacles
		while (spawned < 4) {
            //generate a random location
            position = new Vector3(Random.Range(xPos, xPos + 20f), 0.3f, Random.Range(zPos, zPos + 15f));
            //check if this location overlaps with others, if it does, keep updating the location
            while (inRadius(last, position))
            {
                position = new Vector3(Random.Range(xPos, xPos + 20f), 0.3f, Random.Range(zPos, zPos + 15f));
            }
            Debug.Log(position);
            Debug.Log(inRadius(last, position));
            Instantiate(obstacle_block, position, Quaternion.identity);
            locations.Add(position);
            generateObstacle(position);
            spawned++;
        }


        //spawn Agents
        agents.Add(agent);
        agents.Add(agent_0);
        agents.Add(agent_1);
        agents.Add(agent_2);
        agents.Add(agent_3);
        agents.Add(agent_4);
        agents.Add(agent_5);
        agents.Add(agent_6);


        generateAgents();


		////test for locations
		//for(int i = 0; i < locations.Count; i++){
  //          Debug.Log(locations[i]);
		//}



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool inRadius(Vector3 last, Vector3 now) {
        //radius = 2

        bool inRadius = false;

		if (Math.Abs(last.x - now.x) <= 3 && Math.Abs(last.z - now.z) <= 3)
		{
            inRadius = true;
		}
        return inRadius;
    }

    void generateObstacle(Vector3 center){

        Vector3 loc = new Vector3(0, 0, 0);
        //even:up, odd:down
        int vertical = Random.Range(0, 100);

		if (vertical % 2 == 0 || center.z <= 2) {
            loc = new Vector3(center.x, center.y, center.z + 1);
            Instantiate(obstacle_block, loc, Quaternion.identity);
            locations.Add(loc);
            int horizontal = Random.Range(0, 100);
            if (horizontal % 2 == 0 || center.x <= 3)
            {
                loc = new Vector3(center.x + 1, center.y, center.z);
                Instantiate(obstacle_block, loc, Quaternion.identity);
                locations.Add(loc);
                loc = new Vector3(center.x + 2, center.y, center.z);
                Instantiate(obstacle_block, loc, Quaternion.identity);
                locations.Add(loc);
            }
            else if(horizontal % 2 == 1 || center.x >= 17)
            {
                loc = new Vector3(center.x - 1, center.y, center.z);
                Instantiate(obstacle_block, loc, Quaternion.identity);
                locations.Add(loc);
                loc = new Vector3(center.x - 2, center.y, center.z);
                Instantiate(obstacle_block, loc, Quaternion.identity);
                locations.Add(loc);
            }

        }
        else if (vertical % 2 == 1 || center.z >= 12) {
            loc = new Vector3(center.x, center.y, center.z - 1);
            Instantiate(obstacle_block, loc, Quaternion.identity);
            locations.Add(loc);
            int horizontal = Random.Range(0, 1);
			if (horizontal % 2 == 0 || center.x <= 3)
            {
                loc = new Vector3(center.x + 1, center.y, center.z);
                Instantiate(obstacle_block, loc, Quaternion.identity);
                locations.Add(loc);
            }
            else if(horizontal % 2 == 1 || center.x >= 17)
            {
                loc = new Vector3(center.x - 1, center.y, center.z);
                Instantiate(obstacle_block, loc, Quaternion.identity);
                locations.Add(loc);
            }
        }
    }


    void generateAgents()
    {
        int spawned = 0;

		while (spawned < numAgent)
		{
            Vector3 position = new Vector3(Random.Range(xPos, xPos + 20f), 1f, Random.Range(zPos, zPos + 15f));
            while (!validLoc(position))
            {
                position = new Vector3(Random.Range(xPos, xPos + 20f), 1f, Random.Range(zPos, zPos + 15f));
            }
            Instantiate(agents[spawned % numAgent], position, Quaternion.identity);
            
            spawned++;
        }
        


    }

    bool validLoc(Vector3 loc)
	{
        bool overlap = false;
		for (int i = 0; i < locations.Count; i++) {
			if (Math.Abs(loc.x - locations[i].x) < 2) { overlap = true; }
            if (Math.Abs(loc.z - locations[i].z) < 2) { overlap = true; }
        }
        return overlap;

	}

}
