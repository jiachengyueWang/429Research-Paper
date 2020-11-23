using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Handles Raycast-detection
public class ZeroLayer : MonoBehaviour {
  private Grid grid;
  private AgentNDest ADpair;

  List<int> agentID = new List<int>();
  List<int> destID = new List<int>();
  List<AgentNDest> pairs = new List<AgentNDest>();

  void Start() {
    grid = FindObjectOfType<Grid>();
    ADpair = FindObjectOfType<AgentNDest>();
    agentID = grid.getAgentID();
    destID = grid.getDestID();
    pairs = grid.getPairs();
    
    

  }

  void Update() {
    if (!grid.isCalculating) {

      if (Input.GetKey(KeyCode.Mouse0)){
        grid.CalculatePathExternal(0);
        
        ADpair.Move(grid.get_path(), pairs[0].getAgent());
        // grid.CalculatePathExternal(agentID[0], destID[0]);
      }

      //WJCY
      //update
      if(grid.get_isDone()){
        grid.updatePair(0);
        // grid.updatePair(1);
        // grid.updatePair(2);
        grid.set_isDone(false);
      }

      //wjcy
    }
  }
}