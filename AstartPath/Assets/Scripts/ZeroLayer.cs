using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Handles Raycast-detection
public class ZeroLayer : MonoBehaviour {
  public GameObject otherCube;
  private Grid grid;

  List<int> agentID = new List<int>();
  List<int> destID = new List<int>();

  void Start() {
    grid = FindObjectOfType<Grid>();
    agentID = grid.getAgentID();
    destID = grid.getDestID();
  }

  void Update() {
    if (!grid.isCalculating) {

      if (Input.GetKey(KeyCode.Mouse0)){
        grid.CalculatePathExternal(agentID[0], destID[0]);
        // grid.CalculatePathExternal(agentID[1], destID[1]);
      }

      //WJCY
      //update

      //wjcy
    }
  }
}