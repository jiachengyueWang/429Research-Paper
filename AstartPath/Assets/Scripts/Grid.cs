using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Grid : MonoBehaviour {
  //CELL
  public GameObject cellPrefab;
  public int numberOfCells;
  public int cellsPerRow;
  public int startID;
  public int targetID;
  public bool isCalculating = false;


  public ArrayList allCells;

  private ArrayList openList;
  private ArrayList closedList;
  private Cell startCell;
  private Cell targetCell;

  private Cell cell;


  //WJCY
  //OBSTACLES
  public int numObstacle;
  public GameObject obstaclePrefab;

  //AGENTS & DESTs
  //prefabs database
  List<GameObject> agents = new List<GameObject>();
  List<GameObject> destinations = new List<GameObject>();
  //agents
  public GameObject agent;
  public GameObject agent_0;
  public GameObject agent_1;
  public GameObject agent_2;
  public GameObject agent_3;
  public GameObject agent_4;
  public GameObject agent_5;
  public GameObject agent_6;
  //destinations
  public GameObject dest;
  public GameObject dest_0;
  public GameObject dest_1;
  public GameObject dest_2;
  public GameObject dest_3;
  public GameObject dest_4;
  public GameObject dest_5;
  public GameObject dest_6;

  public int numPair;
  List<int> agentID = new List<int>();
  List<int> destID = new List<int>();


  //GAME LEVEL
  int[] points_0 = new int[]{2, 5, 8, 10, 15, 19 };
  int[] points_1 = new int[]{4, 6, 10, 13, 17, 19};
  int[] points_2 = new int[]{5, 9, 12, 14};
  int[] points_3 = new int[]{3, 6,  9, 13};

  //PATHFINDING
  List<Vector3> allCells_position = new List<Vector3>();
  ArrayList usedCell = new ArrayList();
  ArrayList path = new ArrayList();
  bool isDone = false;
  
  private SetUp setup;
  private AgentNDest ADpair;


  //wjcy

  void Start() {

    setup = FindObjectOfType<SetUp>();
    ADpair = FindObjectOfType<AgentNDest>();


    initialise();
    CreateCells();
    usedCell = setup.generateObstacleCenter();
    createADpair(numPair);
  }

  void Update(){

    // if(!isCalculating){
    //   if(Input.GetKey(KeyCode.Mouse0)){
    //     CalculatePathExternal(agentID[0], destID[0]);
    //     // for(int i = 0; i < numPair; i++){
    //     //   CalculatePathExternal(agentID[i], destID[i]);
    //     // }

    //   }
    // }
    // if (!grid.isCalculating) {
    //   if(Input.GetKey(KeyCode.Mouse0)){
    //     grid.CalculatePathExternal(agent_id, dest_id);

    //   }
      
            

    // }

    // if(grid.get_isDone()){

    //   usedCell.Remove(agent_in_use.transform.position);
    //   agent_in_use.transform.position = dest_in_use.transform.position + new Vector3 (0, 1f, 0);
    //   usedCall.Remove(dest_in_use.transform.position);
    //   dest_in_use.transform.position = setup.nextDestPosition();
    //   grid.set_isDone(false);

    //   }
  }

  //initialise the prefab database
  public void initialise(){
    Debug.Log("initialise");
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
  }




  private void CreateCells() {
    allCells = new ArrayList();

    int zOffset = 0;
    int xOffset = 0;
    int counter = 0;

    for (int i = 0; i < numberOfCells; i++) {
      counter += 1;
      xOffset += 1;
      // Cell newCell = Instantiate(cellPrefab, new Vector3(xOffset, 0, zOffset), transform.rotation) as Cell;
      GameObject newCell = (GameObject)Instantiate(cellPrefab, new Vector3(xOffset, 0, zOffset), transform.rotation);
      newCell.GetComponent<Cell>().id = i;
      allCells.Add(newCell);
      allCells_position.Add(newCell.transform.position);

      if (counter >= cellsPerRow) {
        counter = 0;
        xOffset = 0;
        zOffset += 1;
      }
    }
    createAlcoves(points_0, points_1, points_2, points_3);
  }

  //WJCY

  //return the position of the cell with id
  Vector3 getCell(int id){
    return allCells_position[id];
  }

  //create agent and destination pair
  void createADpair(int numPair){
    int agent_cell_id;
    int dest_cell_id;
    Vector3 agent_position;
    Vector3 dest_position;
    for(int i = 0; i < numPair; i++){
      //create ADpair
      agent_cell_id = Random.Range(0, 373);
      // agent_cell_id = Random.Range(0, 459);
      agent_position = getCell(agent_cell_id);
      while(overlap(agent_position)){
        agent_cell_id = Random.Range(0, 373);
        // agent_cell_id = Random.Range(0, 459);
      }
      agent_position = getCell(agent_cell_id);

      dest_cell_id = Random.Range(0, 373);
      // dest_cell_id = Random.Range(0, 459);
      dest_position = getCell(dest_cell_id);
      while(overlap(dest_position)){
        dest_cell_id = Random.Range(0, 373);
        // dest_cell_id = Random.Range(0, 459);
      }
      dest_position = getCell(dest_cell_id);

      //create a new pair
      GameObject newAgent = (GameObject) Instantiate(agents[i % 7], agent_position + new Vector3 (0, 1, 0), Quaternion.identity);
      GameObject newDest = (GameObject) Instantiate(destinations[i % 7], dest_position, Quaternion.identity);
      AgentNDest newPair = new AgentNDest(newAgent, newDest, agent_cell_id, dest_cell_id);
      

      agentID.Add(agent_cell_id);
      destID.Add(dest_cell_id);
      usedCell.Add(agent_position);
      usedCell.Add(dest_position);
    }
  }

  //check if we chose a valid position to spawn objects
  bool overlap(Vector3 loc){
    bool overlap = false;
    foreach(Vector3 loc_invalid in usedCell){
      if (Math.Abs(loc.x - loc_invalid.x) < 1 && Math.Abs(loc.z - loc_invalid.z) < 1){
        Debug.Log(loc);
        Debug.Log(loc_invalid);
        return true;
      }
    }
    return overlap;
  }

  public void createAlcoves(int[] points_0, int[] points_1, int[] points_2, int[] points_3){
    
    //x-axis UP
    int id = 374;
    alcove_length(points_0, 3, 5, 2, id, true);

    //x-axis BOTTOM
    id = 401;
    alcove_length(points_1, 6, 2, 3, id, false);

    //z-axis LEFT
    id = 425;
    alcove_width(points_2, 4, 3, id, false);

    //z-axis RIGHT
    id = 447;
    alcove_width(points_3, 3, 1, id, true);

  }

  void alcove_length(int[] points, int t0, int t1, int t2, int id, bool up){
    GameObject newCell;
    Vector3 position;

    //1st bump
    int x = points[0];
    while(x < points[1]){
      for(int i = 0; i < t0; i++){
        if(up){
          position = new Vector3 (x, 0, 17 + i);
        }else {
          position = new Vector3 (x, 0, -1 - i);
        }
        
        newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
        newCell.GetComponent<Cell>().id = id;
        id++;
        allCells.Add(newCell);
        allCells_position.Add(newCell.transform.position);

      }
      x++;
    }
    
    //2nd bump
    x = points[2];
    while(x < points[3]){
      for(int i = 0; i < t1; i++){
        if(up){
          position = new Vector3 (x, 0, 17 + i);
        }else {
          position = new Vector3 (x, 0, -1 - i);
        }
        newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
        newCell.GetComponent<Cell>().id = id;
        id++;
        allCells.Add(newCell);
        allCells_position.Add(newCell.transform.position);
      }
      x++;
    }
    
    //3rd bump
    x = points[4];
    while(x < points[5]){
      for(int i = 0; i < t2; i++){
        if(up){
          position = new Vector3 (x, 0, 17 + i);
        }else {
          position = new Vector3 (x, 0, -1 - i);
        }
        newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
        newCell.GetComponent<Cell>().id = id;
        id++;
        allCells.Add(newCell);
        allCells_position.Add(newCell.transform.position);
      }
      x++;
    }
  }

  void alcove_width(int[] points, int t0, int t1, int id, bool right){
    GameObject newCell;
    Vector3 position;

    //1st bump
    int z = points[0];
    while(z < points[1]){
      for(int i = 0; i < t0; i++){
        if(right){
          position = new Vector3 (23 + i, 0, z);
        }else {
          position = new Vector3 (0 - i, 0, z);
        }
        
        newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
        newCell.GetComponent<Cell>().id = id;
        id++;
        allCells.Add(newCell);
        allCells_position.Add(newCell.transform.position);
      }
      z++;
    }

    //2nd bump
    z = points[2];
    while(z < points[3]){
      for(int i = 0; i < t1; i++){
        if(right){
          position = new Vector3 (23 + i, 0, z);
        }else {
          position = new Vector3 (0 - i, 0, z);
        }
        
        newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
        newCell.GetComponent<Cell>().id = id;
        id++;
        allCells.Add(newCell);
        allCells_position.Add(newCell.transform.position);
      }
      z++;
    }
  }

  //wjcy























  public void CalculatePathExternal(int startID, int targetID) {
    // Reset all cells
    foreach (GameObject gameObject in allCells) {
      gameObject.GetComponent<Cell>().Reset();
    }

    StartCoroutine(CalculatePath(startID, targetID));
  }
    private IEnumerator CalculatePath(int startID, int targetID) {

    isCalculating = true;
    
    CreateStart(startID);
    CreateTarget(targetID);

    openList = new ArrayList();
    closedList = new ArrayList();

    Cell currentCell = startCell;
    AddCellToClosedList(currentCell);

    float cycleDelay = 0.0f;
    int cycleCounter = 0;
    while (currentCell.id != targetCell.id) {
      yield return new WaitForSeconds(cycleDelay);

      //safety-abort in case of endless loop
      cycleCounter++;
      if (cycleCounter >= numberOfCells) {
        Debug.Log("No Path Found");
        break;
      }

      //add all cells adjacent to currentCell to openList
      foreach (Cell cell in GetAdjacentCells(currentCell)) {
        float tentativeG = currentCell.G + Vector3.Distance(currentCell.transform.position, cell.transform.position);
        //if cell is on closed list skip to next cycle
        if (cell.onClosedList && tentativeG > cell.G) {
          continue;
        }

        if (!cell.onOpenList || tentativeG < cell.G) {
          cell.CalculateH(targetCell);
          cell.G = tentativeG;
          cell.F = cell.G + cell.H;
          cell.parent = currentCell;

          if (!cell.onClosedList)
            AddCellToOpenList(cell);
        }
      }

      yield return new WaitForSeconds(cycleDelay);

      //get cell with lowest F value from openList, set it to currentCell
      float lowestFValue = 99999.9f;
      foreach (Cell cell in openList) {
        if (cell.F < lowestFValue) {
          lowestFValue = cell.F;
          currentCell = cell;
        }
      }

      //remove currentCell from openList, add to closedList
      openList.Remove(currentCell);
      AddCellToClosedList(currentCell);
    }

    //get path
    // ArrayList path = new ArrayList();
    currentCell = targetCell;
    while (currentCell.id != startCell.id) {
      path.Add(currentCell);
      currentCell = currentCell.parent;
    }
    path.Add(currentCell);
    path.Reverse();

    //draw path
    LineRenderer lineRenderer = GetComponent<LineRenderer>();
    lineRenderer.positionCount = path.Count;
    // lineRenderer.SetVertexCount(path.Count);
    int i = 0;
    foreach (Cell cell in path) {
      lineRenderer.SetPosition(i, cell.transform.position + new Vector3(0, 0.5f, 0));
      i++;
    }

    isCalculating = false;
    
    //WJCY

    //after found the path and drew it, update to the next 
    isDone = true;
    //wjcy

  }

  private ArrayList GetAdjacentCells(Cell currentCell) {
    return currentCell.GetAdjacentCells(allCells, cellsPerRow);
  }

  private void AddCellToClosedList(Cell currentCell) {
    closedList.Add(currentCell);
    currentCell.SetToClosedList();
  }

  private void AddCellToOpenList(Cell currentCell) {
    openList.Add(currentCell);
    currentCell.SetToOpenList();
  }

  private void CreateStart(int id) {
    GameObject tempStartCell = (GameObject)allCells[id];
    startCell = tempStartCell.GetComponent<Cell>();
    startCell.SetToStart();
  }

  private void CreateTarget(int id) {
    GameObject tempTargetCell = (GameObject)allCells[id];
    targetCell = tempTargetCell.GetComponent<Cell>();
    targetCell.SetToTarget();
  }


  //helpers
  public ArrayList get_path(){
    return this.path;
  }

  public bool get_isDone(){
    return this.isDone;
  }

  public void set_isDone(bool b){
    isDone = b;
  }

  public int[] get_points(int id){
    if(id == 0){
      return this.points_0;
    }else if(id == 1){
      return this.points_1;
    }else if(id == 2){
      return this.points_2;
    }else{
      return this.points_3;
    }
  }

  public List<int> getAgentID(){
    return this.agentID;
  }
  public List<int> getDestID(){
    return this.destID;
  }
}







































































































// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// public class Grid : MonoBehaviour {
//   public GameObject cellPrefab;
//   public int numberOfCells;
//   public int cellsPerRow;
//   public int startID;
//   public int targetID;
//   public bool isCalculating = false;

//   public ArrayList allCells;

//   private ArrayList openList;
//   private ArrayList closedList;
//   private Cell startCell;
//   private Cell targetCell;

//   //WJCY

//   int[] points_0 = new int[]{2, 5, 8, 10, 15, 19 };
//   int[] points_1 = new int[]{4, 6, 10, 13, 17, 19};
//   int[] points_2 = new int[]{5, 9, 12, 14};
//   int[] points_3 = new int[]{3, 6,  9, 13};
//   ArrayList path = new ArrayList();
//   bool isDone = false;
//   private SetUp setup;
//   // private Dictionary<GameObject, bool> path_done = new Dictionary<GameObject, bool>();
//   private List<bool> path_done = new List<bool>();
//   int numPair;


//   //wjcy

//   void Start() {
//     CreateCells();
//     setup = FindObjectOfType<SetUp>();
//     numPair = setup.numAgent;
//   }

//   public ArrayList get_path(){
//     return this.path;
//   }

//   public bool get_isDone(){
//     return this.isDone;
//   }

//   public void set_isDone(bool b){
//     isDone = b;
//   }


//   public int[] get_points(int id){
//     if(id == 0){
//       return this.points_0;
//     }else if(id == 1){
//       return this.points_1;
//     }else if(id == 2){
//       return this.points_2;
//     }else{
//       return this.points_3;
//     }
//   }


//   private void CreateCells() {
//     allCells = new ArrayList();

//     int zOffset = 0;
//     int xOffset = 0;
//     int counter = 0;

//     for (int i = 0; i < numberOfCells; i++) {
//       counter += 1;
//       xOffset += 1;
//       //GameObject newCell = (GameObject)Instantiate(cellPrefab, new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z + zOffset), transform.rotation);
//       GameObject newCell = (GameObject)Instantiate(cellPrefab, new Vector3(xOffset, 0, zOffset), transform.rotation);
//       newCell.GetComponent<Cell>().id = i;
//       allCells.Add(newCell);

//       if (counter >= cellsPerRow) {
//         counter = 0;
//         xOffset = 0;
//         zOffset += 1;
//       }
//     }

//     createAlcoves(points_0, points_1, points_2, points_3);
//   }


//   //WJCY
//   public void createAlcoves(int[] points_0, int[] points_1, int[] points_2, int[] points_3){
//     int id = 374;
//     //x-axis UP
//     alcove_length(points_0, 3, 5, 2, id, true);

//     //x-axis BOTTOM
//     alcove_length(points_1, 6, 2, 3, id, false);

//     //z-axis LEFT
//     alcove_width(points_2, 4, 3, id, false);

//     //z-axis RIGHT
//     alcove_width(points_3, 3, 1, id, true);

//   }

//   void alcove_length(int[] points, int t0, int t1, int t2, int id, bool up){
//     GameObject newCell;
//     Vector3 position;

//     //1st bump
//     int x = points[0];
//     while(x < points[1]){
//       for(int i = 0; i < t0; i++){
//         if(up){
//           position = new Vector3 (x, 0, 17 + i);
//         }else {
//           position = new Vector3 (x, 0, -1 - i);
//         }
        
//         newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
//         newCell.GetComponent<Cell>().id = id;
//         id++;
//         allCells.Add(newCell);
//       }
//       x++;
//     }
    
//     //2nd bump
//     x = points[2];
//     while(x < points[3]){
//       for(int i = 0; i < t1; i++){
//         if(up){
//           position = new Vector3 (x, 0, 17 + i);
//         }else {
//           position = new Vector3 (x, 0, -1 - i);
//         }
//         newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
//         newCell.GetComponent<Cell>().id = id;
//         id++;
//         allCells.Add(newCell);
//       }
//       x++;
//     }
    
//     //3rd bump
//     x = points[4];
//     while(x < points[5]){
//       for(int i = 0; i < t2; i++){
//         if(up){
//           position = new Vector3 (x, 0, 17 + i);
//         }else {
//           position = new Vector3 (x, 0, -1 - i);
//         }
//         newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
//         newCell.GetComponent<Cell>().id = id;
//         id++;
//         allCells.Add(newCell);
//       }
//       x++;
//     }
//   }

//   void alcove_width(int[] points, int t0, int t1, int id, bool right){
//     GameObject newCell;
//     Vector3 position;

//     //1st bump
//     int z = points[0];
//     while(z < points[1]){
//       for(int i = 0; i < t0; i++){
//         if(right){
//           position = new Vector3 (23 + i, 0, z);
//         }else {
//           position = new Vector3 (0 - i, 0, z);
//         }
        
//         newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
//         newCell.GetComponent<Cell>().id = id;
//         id++;
//         allCells.Add(newCell);
//       }
//       z++;
//     }

//     //2nd bump
//     z = points[2];
//     while(z < points[3]){
//       for(int i = 0; i < t1; i++){
//         if(right){
//           position = new Vector3 (23 + i, 0, z);
//         }else {
//           position = new Vector3 (0 - i, 0, z);
//         }
        
//         newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
//         newCell.GetComponent<Cell>().id = id;
//         id++;
//         allCells.Add(newCell);
//       }
//       z++;
//     }


//   }
//   //wjcy


//   public void CalculatePathExternal() {
//     // Reset all cells
//     foreach (GameObject gameObject in allCells) {
//       gameObject.GetComponent<Cell>().Reset();
//     }

//     StartCoroutine(CalculatePath());
//   }
//     private IEnumerator CalculatePath() {

//     isCalculating = true;
    
//     CreateStart(startID);
//     CreateTarget(targetID);

//     openList = new ArrayList();
//     closedList = new ArrayList();

//     Cell currentCell = startCell;
//     AddCellToClosedList(currentCell);

//     float cycleDelay = 0.0f;
//     int cycleCounter = 0;
//     while (currentCell.id != targetCell.id) {
//       yield return new WaitForSeconds(cycleDelay);

//       //safety-abort in case of endless loop
//       cycleCounter++;
//       if (cycleCounter >= numberOfCells) {
//         Debug.Log("No Path Found");
//         break;
//       }

//       //add all cells adjacent to currentCell to openList
//       foreach (Cell cell in GetAdjacentCells(currentCell)) {
//         float tentativeG = currentCell.G + Vector3.Distance(currentCell.transform.position, cell.transform.position);
//         //if cell is on closed list skip to next cycle
//         if (cell.onClosedList && tentativeG > cell.G) {
//           continue;
//         }

//         if (!cell.onOpenList || tentativeG < cell.G) {
//           cell.CalculateH(targetCell);
//           cell.G = tentativeG;
//           cell.F = cell.G + cell.H;
//           cell.parent = currentCell;

//           if (!cell.onClosedList)
//             AddCellToOpenList(cell);
//         }
//       }

//       yield return new WaitForSeconds(cycleDelay);

//       //get cell with lowest F value from openList, set it to currentCell
//       float lowestFValue = 99999.9f;
//       foreach (Cell cell in openList) {
//         if (cell.F < lowestFValue) {
//           lowestFValue = cell.F;
//           currentCell = cell;
//         }
//       }

//       //remove currentCell from openList, add to closedList
//       openList.Remove(currentCell);
//       AddCellToClosedList(currentCell);
//     }

//     //get path
//     // ArrayList path = new ArrayList();
//     currentCell = targetCell;
//     while (currentCell.id != startCell.id) {
//       path.Add(currentCell);
//       currentCell = currentCell.parent;
//     }
//     path.Add(currentCell);
//     path.Reverse();

//     //draw path
//     LineRenderer lineRenderer = GetComponent<LineRenderer>();
//     lineRenderer.positionCount = path.Count;
//     // lineRenderer.SetVertexCount(path.Count);
//     int i = 0;
//     foreach (Cell cell in path) {
//       lineRenderer.SetPosition(i, cell.transform.position + new Vector3(0, 0.5f, 0));
//       i++;
//     }

//     isCalculating = false;
    
//     //WJCY

//     //after found the path and drew it, update to the next 
//     isDone = true;
//     //wjcy





//   }

//   // private IEnumerator CalculatePath() {

//   //   isCalculating = true;

//   //   CreateStart(startID);
//   //   CreateTarget(targetID);

//   //   openList = new ArrayList();
//   //   closedList = new ArrayList();

//   //   Cell currentCell = startCell;
//   //   AddCellToClosedList(currentCell);

//   //   float cycleDelay = 0.0f;
//   //   int cycleCounter = 0;
//   //   while (currentCell.id != targetCell.id) {
//   //     yield return new WaitForSeconds(cycleDelay);

//   //     //safety-abort in case of endless loop
//   //     cycleCounter++;
//   //     if (cycleCounter >= numberOfCells) {
//   //       Debug.Log("No Path Found");
//   //       break;
//   //     }

//   //     //add all cells adjacent to currentCell to openList
//   //     foreach (Cell cell in GetAdjacentCells(currentCell)) {
//   //       float tentativeG = currentCell.G + Vector3.Distance(currentCell.transform.position, cell.transform.position);
//   //       //if cell is on closed list skip to next cycle
//   //       if (cell.onClosedList && tentativeG > cell.G) {
//   //         continue;
//   //       }

//   //       if (!cell.onOpenList || tentativeG < cell.G) {
//   //         cell.CalculateH(targetCell);
//   //         cell.G = tentativeG;
//   //         cell.F = cell.G + cell.H;
//   //         cell.parent = currentCell;

//   //         if (!cell.onClosedList)
//   //           AddCellToOpenList(cell);
//   //       }
//   //     }

//   //     yield return new WaitForSeconds(cycleDelay);

//   //     //get cell with lowest F value from openList, set it to currentCell
//   //     float lowestFValue = 99999.9f;
//   //     foreach (Cell cell in openList) {
//   //       if (cell.F < lowestFValue) {
//   //         lowestFValue = cell.F;
//   //         currentCell = cell;
//   //       }
//   //     }

//   //     //remove currentCell from openList, add to closedList
//   //     openList.Remove(currentCell);
//   //     AddCellToClosedList(currentCell);
//   //   }

//   //   //get path
//   //   // ArrayList path = new ArrayList();
//   //   currentCell = targetCell;
//   //   while (currentCell.id != startCell.id) {
//   //     path.Add(currentCell);
//   //     currentCell = currentCell.parent;
//   //   }
//   //   path.Add(currentCell);
//   //   path.Reverse();

//   //   //draw path
//   //   LineRenderer lineRenderer = GetComponent<LineRenderer>();
//   //   lineRenderer.positionCount = path.Count;
//   //   // lineRenderer.SetVertexCount(path.Count);
//   //   int i = 0;
//   //   foreach (Cell cell in path) {
//   //     lineRenderer.SetPosition(i, cell.transform.position + new Vector3(0, 0.5f, 0));
//   //     i++;
//   //   }

//   //   isCalculating = false;
    
//   //   //WJCY

//   //   //after found the path and drew it, update to the next 
//   //   isDone = true;
//   //   //wjcy





//   // }

//   private ArrayList GetAdjacentCells(Cell currentCell) {
//     return currentCell.GetAdjacentCells(allCells, cellsPerRow);
//   }

//   private void AddCellToClosedList(Cell currentCell) {
//     closedList.Add(currentCell);
//     currentCell.SetToClosedList();
//   }

//   private void AddCellToOpenList(Cell currentCell) {
//     openList.Add(currentCell);
//     currentCell.SetToOpenList();
//   }

//   private void CreateStart(int id) {
//     GameObject tempStartCell = (GameObject)allCells[id];
//     startCell = tempStartCell.GetComponent<Cell>();
//     startCell.SetToStart();
//   }

//   private void CreateTarget(int id) {
//     GameObject tempTargetCell = (GameObject)allCells[id];
//     targetCell = tempTargetCell.GetComponent<Cell>();
//     targetCell.SetToTarget();
//   }
// }