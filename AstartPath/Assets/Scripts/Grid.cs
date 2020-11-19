using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
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

  //WJCY
  int[] pointsL;
  int[] pointsW;


  //wjcy

  void Start() {
    CreateCells();
  }


  private void CreateCells() {
    allCells = new ArrayList();

    int zOffset = 0;
    int xOffset = 0;
    int counter = 0;

    for (int i = 0; i < numberOfCells; i++) {
      counter += 1;
      xOffset += 1;
      //GameObject newCell = (GameObject)Instantiate(cellPrefab, new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z + zOffset), transform.rotation);
      GameObject newCell = (GameObject)Instantiate(cellPrefab, new Vector3(xOffset, 0, zOffset), transform.rotation);
      newCell.GetComponent<Cell>().id = i;
      allCells.Add(newCell);

      if (counter >= cellsPerRow) {
        counter = 0;
        xOffset = 0;
        zOffset += 1;
      }
    }

    createAlcoves(pointsL, pointsW);
  }


  //WJCY
  public void createAlcoves(int[] pointsL, int[] pointsW){
    int id = 374;
    //x-axis UP
    pointsL = new int[]{ 2, 5, 8, 10, 15, 19 };
    alcove_length(pointsL, 3, 5, 2, id, true);

    //x-axis BOTTOM
    pointsL = new int[]{4, 6, 10, 13, 17, 19};
    alcove_length(pointsL, 6, 2, 3, id, false);

    //z-axis LEFT
    pointsW = new int[]{5, 9, 12, 14};
    alcove_width(pointsW, 4, 3, id, false);

    //z-axis RIGHT
    pointsW = new int[]{3, 6,  9, 13};
    alcove_width(pointsW, 3, 1, id, true);

  }

  void alcove_length(int[] points, int t0, int t1, int t2, int id, bool up){
    GameObject newCell;
    Vector3 position;

    //1st bump
    int x = points[0];
    while(x < points[1]){
      for(int i = 0; i < t0; i++){
        if(up){
          position = new Vector3 (x, 0, 16 + i);
        }else {
          position = new Vector3 (x, 0, 0 - i);
        }
        
        newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
        newCell.GetComponent<Cell>().id = id;
        id++;
        allCells.Add(newCell);
      }
      x++;
    }
    
    //2nd bump
    x = points[2];
    while(x < points[3]){
      for(int i = 0; i < t1; i++){
        if(up){
          position = new Vector3 (x, 0, 16 + i);
        }else {
          position = new Vector3 (x, 0, 0 - i);
        }
        newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
        newCell.GetComponent<Cell>().id = id;
        id++;
        allCells.Add(newCell);
      }
      x++;
    }
    
    //3rd bump
    x = points[4];
    while(x < points[5]){
      for(int i = 0; i < t2; i++){
        if(up){
          position = new Vector3 (x, 0, 16 + i);
        }else {
          position = new Vector3 (x, 0, 0 - i);
        }
        newCell = (GameObject)Instantiate(cellPrefab, position, transform.rotation);
        newCell.GetComponent<Cell>().id = id;
        id++;
        allCells.Add(newCell);
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
      }
      z++;
    }


  }





  //wjcy




  public void CalculatePathExternal() {
    // Reset all cells
    foreach (GameObject gameObject in allCells) {
      gameObject.GetComponent<Cell>().Reset();
    }

    StartCoroutine(CalculatePath());
  }

  private IEnumerator CalculatePath() {
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
    ArrayList path = new ArrayList();
    currentCell = targetCell;
    while (currentCell.id != startCell.id) {
      path.Add(currentCell);
      currentCell = currentCell.parent;
    }
    path.Add(currentCell);
    path.Reverse();

    //draw path
    LineRenderer lineRenderer = GetComponent<LineRenderer>();
    lineRenderer.SetVertexCount(path.Count);
    int i = 0;
    foreach (Cell cell in path) {
      lineRenderer.SetPosition(i, cell.transform.position + new Vector3(0, 1, 0));
      i++;
    }

    isCalculating = false;
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
}