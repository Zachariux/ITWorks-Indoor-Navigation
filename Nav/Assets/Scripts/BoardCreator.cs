using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class BoardCreator : MonoBehaviour
{
    // The type of tile that will be laid in a specific position.

    public int columns;                                 // The number of columns on the board (how wide it will be).
    public int rows;                                    // The number of rows on the board (how tall it will be).	

    public GameObject[] floorTiles;                           // An array of floor tile prefabs.
    public GameObject[] wallTiles;
    Node[,] graph;
    public TileType[] tileTypes;
    int[,] tiles;
    public GameObject start;
    public GameObject end;
    int currLocationx;
    int currLocationy;

    int destinationY;
    int destinationX;

    public LineRenderer drawPath;

    public static BoardCreator instance = null;             //Static instance of GameManager which allows it to be accessed by any other script.


    public GameObject TafeSAMap;
    private Texture2D myTexture;
    private Sprite mySprite;
    public Player PointPositions;

    public Button Up;
    public Button Down;

    public Text Error;


    public static List<Node> currentPath;
    public static List<Node> currentPathB;
    public static List<Node> currentPathG;
    public static List<Node> currentPath1F;
    public static List<Node> currentPath2F;
    public static List<Node> currentPath3F;
    public static List<Node> currentPath4F;

    public GameObject DropdownControlHolder;
    public DropDown DropdownControl;

    public int FloorNum;


    private void Start()
    {
        DropdownControl = DropdownControlHolder.GetComponent<DropDown>();
        Error.gameObject.SetActive(false);
        Up.onClick.AddListener(TaskOnClickUp);
        Down.onClick.AddListener(TaskOnClickDown);
        PointPositions = start.GetComponent<Player>();
        Setup();
        FloorNum = 1;
    }

    void TaskOnClickDown()
    {

        if (FloorNum != 0)
        {
            if (drawPath != null)
            {
                drawPath.positionCount = 0;
            }

            FloorNum--;
            GlobalControl.FloorNum = FloorNum;
            DrawCurrentFloorPath();
        }


        //Debug.Log("currentPathG" + currentPathG.ToArray().Length);
        // Debug.Log("currentPathB"+currentPathB.ToArray().Length);

    }
    void TaskOnClickUp()
    {
        if (FloorNum != 5)
        {
            if (drawPath != null)
            {
                drawPath.positionCount = 0;
            }
     FloorNum++;
            GlobalControl.FloorNum = FloorNum;
            DrawCurrentFloorPath();



        }


   

    }



    public void CreatePath()
    {
        Setup();
        GeneratePathfindingGraph();
        GenerateMapData();
        Error.gameObject.SetActive(false);

        if (UnitCanEnterTile(PointPositions.DestinationX, PointPositions.DestinationY) == false)
        {
            // Make sure markers are over walkable paths
            Debug.Log("ERROR");
            Error.gameObject.SetActive(true);
            Error.color = Color.blue;
            return;
        }
        if (UnitCanEnterTile(PointPositions.xPosRounded, PointPositions.yPosRounded) == false)
        {
            // Make sure markers are over walkable paths
            Debug.Log("ERROR");
            Error.gameObject.SetActive(true);
            Error.color = Color.red;
            return;
        }



        DrawMap();

    }

    public void Setup()
    {

        //Set which map image
        mySprite = TafeSAMap.GetComponent<SpriteRenderer>().sprite;
        myTexture = mySprite.texture;






        //Get starting pointer position and rounds it to closest node

        PointPositions = start.GetComponent<Player>();
        currLocationx = Mathf.RoundToInt(start.transform.position.x);
        currLocationy = Mathf.RoundToInt(start.transform.position.y);

        destinationX = Mathf.RoundToInt(end.transform.position.x);
        destinationY = Mathf.RoundToInt(end.transform.position.y);

        PointPositions.xPosRounded = currLocationx;
        PointPositions.yPosRounded = currLocationy;

        PointPositions.DestxPosRounded = destinationX;
        PointPositions.DestyPosRounded = destinationY;

        PointPositions.DestinationX = Mathf.RoundToInt(end.transform.position.x);
        PointPositions.DestinationY = Mathf.RoundToInt(end.transform.position.y);






    }


    public void DrawMap()
    {

        GeneratePathTo(PointPositions.DestinationX, PointPositions.DestinationY, PointPositions.gameObject);


        //Draw path using line renderer
        drawPath = GetComponent<LineRenderer>();
        if (drawPath.enabled == false)
        {
            drawPath.enabled = true;
        }
        int currentPathCount = (PointPositions.currentPath.Count);
        drawPath.positionCount = currentPathCount;

        for (int i = 0; i < currentPathCount; i++)
        {
            drawPath.SetPosition(i, new Vector3(PointPositions.currentPath[i].x, start.GetComponent<Player>().currentPath[i].y, 0));
        }
    }

    public void GenerateMapData()
    {
        // Allocate our map tiles
        tiles = new int[columns, rows];

        int x, y;
        float pixel2units = mySprite.rect.width / mySprite.bounds.size.x;

        // Initialize our map tiles to be grass
        for (x = 0; x < columns; x++)
        {
            for (y = 0; y < rows; y++)
            {
                tiles[x, y] = 0;
            }
        }

        // Make a big swamp area
        for (x = 0; x < columns; x++)
        {
            for (y = 0; y < rows; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];
                tt.tileVisualPrefab.GetComponent<SpriteRenderer>().color = myTexture.GetPixel(Mathf.RoundToInt(x * pixel2units / 2), Mathf.RoundToInt(y * pixel2units / 2));
                if (System.Math.Round(tt.tileVisualPrefab.GetComponent<SpriteRenderer>().color.r, 3) <= 0.750)
                {
                    tiles[x, y] = 1;

                }
            }
        }









    }

    public void GenerateMapVisual()
    {
        GameObject objToSpawn;

        objToSpawn = new GameObject("Visual of nodes");

        //  float pixel2units = mySprite.rect.width / mySprite.bounds.size.x;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {


                TileType tt = tileTypes[tiles[x, y]];
                if (!tt.isWalkable)
                {

                }
                else
                {
                    //   GameObject go = (GameObject)Instantiate(floorTiles[0], new Vector3(x, y, 0), Quaternion.identity);


                    //     go.transform.parent = objToSpawn.transform;
                    //     go.name = "x = " + x + " y = " + y;
                }


            }
        }

    }










    public int TileIsPassable(int x, int y)
    {

        return tiles[x, y];

    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {

        float cost = 001f;

        if (sourceX != targetX && sourceY != targetY)
        {

            // Purely a cosmetic thing
            cost += 0.001f;
        }
        if (TileIsPassable(sourceX, sourceY) == 1)
        {
            // We are moving diagonally Fudge the cost for tie-breaking
            // Purely a cosmetic thing
            cost = 999999f;
        }
        return cost;
    }





    public void GeneratePathfindingGraph()
    {

        // Initialize the array
        graph = new Node[columns, rows];

        // Initialize a Node for each spot in the array
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        // Now that all the nodes exist, calculate their neighbours
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {



                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                    if (y < rows - 1)
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                }

                // Try Right
                if (x < columns - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    if (y < rows - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                }

                // Try straight up and down
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < rows - 1)
                    graph[x, y].neighbours.Add(graph[x, y + 1]);




            }
        }



    }


    public bool UnitCanEnterTile(int x, int y)
    {

        // We could test the unit's walk/hover/fly type against various
        // terrain flags here to see if they are allowed to enter the tile.

        return tileTypes[tiles[x, y]].isWalkable;
        // return true;
    }


    public void ResetAllPaths()
    {
        currentPath = null;
    currentPathB = null;
    currentPathG = null;
    currentPath1F = null;
    currentPath2F = null;
    currentPath3F = null;
    currentPath4F = null;
}


    public void GeneratePathTo(int x, int y, GameObject Object)
    {

        //player.GetComponent<Path> ().currentPath = null;
        if (UnitCanEnterTile(x, y) == false)
        {
            // We probably clicked on a mountain or something, so just quit out.
            Debug.Log("ERROR");
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        // Setup the "Q" -- the list of nodes we haven't checked yet.
        List<Node> unvisited = new List<Node>();

        Node source = graph[
            (int)start.GetComponent<Player>().xPosRounded,
            (int)start.GetComponent<Player>().yPosRounded
                      //(int)Object.transform.position.x,
                      //(int)Object.transform.position.y

                      ];
        Node target = graph[
                          x,
                          y
                      ];

        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }
        while (unvisited.Count > 0)
        {
            // "u" is going to be the unvisited node with the smallest distance.
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;  // Exit the while loop!
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        // If we get there, the either we found the shortest route
        // to our target, or there is no route at ALL to our target.

        if (prev[target] == null)
        {
            // No route between our target and the source
            Debug.Log("No route between our target and the source");
            return;
        }


        currentPath = new List<Node>();




        Node curr = target;

        // Step through the "prev" chain and add it to our path
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        // Right now, currentPath describes a route from out target to our source
        // So we need to invert it!

        switch (FloorNum)
        {
            case 5:

                currentPath4F = currentPath;

                Debug.Log(GlobalControl.FloorLevel.ToString());
                break;
            case 4:

                currentPath3F = currentPath;

                Debug.Log(GlobalControl.FloorLevel.ToString());
                break;
            case 3:

                currentPath2F = currentPath;

                Debug.Log(GlobalControl.FloorLevel.ToString());
                break;
            case 2:

                currentPath1F = currentPath;

                Debug.Log(GlobalControl.FloorLevel.ToString());
                break;
            case 1:

                currentPathG = currentPath;

                Debug.Log(GlobalControl.FloorLevel.ToString());

                break;
            default:

                currentPathB = currentPath;

                Debug.Log(GlobalControl.FloorLevel.ToString());
                break;
        }


        Object.GetComponent<Player>().currentPath = currentPath;





        for (int i = 0; i < currentPath.Count; i++)
        {
            //Debug.Log(currentPath[i].x + " - " + currentPath[i].y);
            //            Debug.Log(currentPathG[i].x + " - " + currentPathG[i].y);

            // Debug.DrawLine(new Vector3(currentPath[i].x, currentPath[i + 1].y, -1), new Vector3(currentPath[i + 1].x, currentPath[i + 1].y, -1), Color.green);
        }
        //GameObject.FindGameObjectWithTag ("Player").GetComponent<Unit> ().currentPath = currentPath;

        //player.GetComponent<Unit> ().currentPath = currentPath;

    }

    public void ResetPointers()
    {
        
        start.transform.position = new Vector3(90, 10, 1);
        end.transform.position = new Vector3(90, 15, 1);
    }

    public void DrawCurrentFloorPath()
    {
        List<Node> currentFloorPath = new List<Node>();

        switch (FloorNum)
        {
            case 5:

                currentFloorPath = currentPath4F;

                break;
            case 4:

                currentFloorPath = currentPath3F;
                break;
            case 3:

                currentFloorPath = currentPath2F;
                break;
            case 2:
                currentFloorPath = currentPath1F;

                break;
            case 1:

                currentFloorPath = currentPathG;

                break;
            default:
                currentFloorPath = currentPathB;

                break;
        }




        if (currentFloorPath != null)
        {
            //Draw path using line renderer
            drawPath = GetComponent<LineRenderer>();
            int currentPathCount = (currentFloorPath.Count);
            drawPath.positionCount = currentPathCount;

            for (int i = 0; i < currentPathCount; i++)
            {
                drawPath.SetPosition(i, new Vector3(currentFloorPath[i].x, currentFloorPath[i].y, 0));
            }
        }
    }


}

