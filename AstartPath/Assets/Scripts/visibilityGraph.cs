using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visibilityGraph : MonoBehaviour
{
    public GameObject vertexPrefab;

    //alcove points
    int[] points_0 = new int[]{2, 5, 8, 10, 15, 19 };
    int[] points_1 = new int[]{4, 6, 10, 13, 17, 19};
    int[] points_2 = new int[]{5, 9, 12, 14};
    int[] points_3 = new int[]{3, 6,  9, 13};

    int[] thick_0 = new int[3]{3, 5, 2};
    int[] thick_1 = new int[3]{6, 2, 3};
    int[] thick_2 = new int[2]{4, 3};
    int[] thick_3 = new int[2]{3, 1};

    // private Grid grid;
    private SetUp setup;
    //graph points only contains alcove points
    List<Vector3> graph_points = new List<Vector3>();
    List<GameObject> graph_vertices = new List<GameObject>();

    void Start()
    {
        setup = FindObjectOfType<SetUp>();
    }

    //set up the alcove points (static points)
    public List<Vector3> graph_vertex_setup(){
        Vector3 myPoint;
        int thick_index = 0;
        //get all the vertices
        // myPoint = new Vector3(points_0[0] - 0.5f, 0.5f, 16.5f);
        // graph_points.Add(myPoint);
        for(int i = 0; i < 6; i = i + 2){

            myPoint = new Vector3(points_0[i] - 0.5f, 0.5f, 16.5f);
            graph_points.Add(myPoint);
            myPoint = new Vector3(points_0[i + 1] - 0.5f, 0.5f, 16.5f);
            graph_points.Add(myPoint);
            myPoint = new Vector3(points_0[i] - 0.5f, 0.5f, 16.5f + thick_0[thick_index]);
            graph_points.Add(myPoint);
            myPoint = new Vector3(points_0[i + 1] - 0.5f, 0.5f, 16.5f + thick_0[thick_index]);
            graph_points.Add(myPoint);

            myPoint = new Vector3(points_1[i] - 0.5f, 0.5f, -0.5f);
            graph_points.Add(myPoint);
            myPoint = new Vector3(points_1[i + 1] - 0.5f, 0.5f, -0.5f);
            graph_points.Add(myPoint);
            myPoint = new Vector3(points_1[i] - 0.5f, 0.5f, -0.5f - thick_1[thick_index]);
            graph_points.Add(myPoint);
            myPoint = new Vector3(points_1[i + 1] - 0.5f, 0.5f, -0.5f - thick_1[thick_index]);
            graph_points.Add(myPoint);
            thick_index++;
        }

        thick_index = 0;
        for(int i = 0; i < 4; i = i + 2){
            myPoint = new Vector3(0.5f, 0.5f, points_2[i] - 0.5f);
            graph_points.Add(myPoint);
            myPoint = new Vector3(0.5f, 0.5f, points_2[i + 1] - 0.5f);
            graph_points.Add(myPoint);
            myPoint = new Vector3(0.5f - thick_2[thick_index], 0.5f, points_2[i] - 0.5f);
            graph_points.Add(myPoint);
            myPoint = new Vector3(0.5f - thick_2[thick_index], 0.5f, points_2[i + 1] - 0.5f);
            graph_points.Add(myPoint);

            myPoint = new Vector3(22.5f, 0.5f, points_3[i] - 0.5f);
            graph_points.Add(myPoint);
            myPoint = new Vector3(22.5f, 0.5f, points_3[i + 1] - 0.5f);
            graph_points.Add(myPoint);
            myPoint = new Vector3(22.5f + thick_3[thick_index], 0.5f, points_3[i] - 0.5f);
            graph_points.Add(myPoint);
            myPoint = new Vector3(22.5f + thick_3[thick_index], 0.5f, points_3[i + 1] - 0.5f);
            graph_points.Add(myPoint);
            thick_index++;
        }
        return this.graph_points;
    }

    public void instantiate_vertex(List<Vector3> points, bool alcove){
        if(alcove){
            foreach(Vector3 vertex in points){
                graph_vertices.Add(Instantiate(vertexPrefab, vertex, Quaternion.identity));
            }
        }else {
            foreach(Vector3 vertex in points){
                Instantiate(vertexPrefab, vertex, Quaternion.identity);
            }  
        } 
    }

    public void Draw_graph(List<Vector3> obstacle_graph_points){
        
        // List<Vector3> pointList = new List<Vector3>();
        for(int i = 0; i < graph_vertices.Count; i++){
            LineRenderer lr = graph_vertices[i].GetComponent<LineRenderer>();
            List<Vector3> pointList = new List<Vector3>();
            for(int j = 0; j < obstacle_graph_points.Count; j++){
                if (!Physics.Linecast (graph_vertices[i].transform.position, obstacle_graph_points[j])){
                    pointList.Add(obstacle_graph_points[j]);
                    pointList.Add(graph_vertices[i].transform.position);   
                }
                lr.positionCount = pointList.Count;
                int a = 0;
                foreach(Vector3 point in pointList){
                    lr.SetPosition(a, point);
                    a++;
                }
            }
        }
    }


    public static bool isIntersecting(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4){
	    bool isIntersecting = false;

	    if (IsPointsOnDifferentSides(p1, p2, p3, p4) && IsPointsOnDifferentSides(p3, p4, p1, p2))
	    	isIntersecting = true;

	    return isIntersecting;
    }

    //Are the points on different sides of a line?
    private static bool IsPointsOnDifferentSides(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4){
	    bool isOnDifferentSides = false;
	
	    //The direction of the line
	    Vector3 lineDir = p2 - p1;

	    //The normal to a line is just flipping x and z and making z negative
        Vector3 lineNormal = new Vector3(-lineDir.z, lineDir.y, lineDir.x);
	
	    //Now we need to take the dot product between the normal and the points on the other line
	    float dot1 = Vector3.Dot(lineNormal, p3 - p1);
	    float dot2 = Vector3.Dot(lineNormal, p4 - p1);

	    //If you multiply them and get a negative value then p3 and p4 are on different sides of the line
	    if (dot1 * dot2 < 0f)
		    isOnDifferentSides = true;

	    return isOnDifferentSides;
    }
}
