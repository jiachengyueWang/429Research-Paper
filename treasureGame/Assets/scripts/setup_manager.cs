using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setup_manager : MonoBehaviour
{

    public GameObject rock;
    public GameObject crate;


    private List<GameObject> rocks = new List<GameObject>();
    private List<GameObject> crates = new List<GameObject>();





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generate_crate_rock(){
        Vector3 position = new Vector3(Random.Range(-24, 24), 0, Random.Range(-24, 24));

        while(overlap){
            position = new Vector3(Random.Range(-24, 24), 0, Random.Range(-24, 24));
        }
    }

    bool overlap(Vector3 position){
        bool overlap;

        return overlap;
    }
}
