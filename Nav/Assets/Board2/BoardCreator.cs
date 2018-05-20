using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    // The type of tile that will be laid in a specific position.
   
    public enum Faces
    {
        Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft, Center, Floor
    }


    public int columns;                                 // The number of columns on the board (how wide it will be).
    public int rows;                                    // The number of rows on the board (how tall it will be).	

    public GameObject[] floorTiles;                           // An array of floor tile prefabs.
    public GameObject[] wallTiles;
    Node[,] graph;
    public TileType[] tileTypes;
    int[,] tiles;

    private GameObject boardHolder;                           // GameObject that acts as a container for all other tiles.
    public GameObject player;
    public GameObject end;
    int currLocationx;
    int currLocationy;

    public LineRenderer drawPath;

    public static BoardCreator instance = null;             //Static instance of GameManager which allows it to be accessed by any other script.

    public GameObject TafeSAMap;
    private Texture2D myTexture;
    private Sprite mySprite;
    public Player p;
    private void Start()
    {
        boardHolder = new GameObject("BoardHolder");
        Setup();
        //player.GetComponent<Unit>().map = this;





        //GenerateMapData();
        //  GeneratePathfindingGraph();
        //GenerateMapVisual();

        //InstantiateTiles();



    }

    public void Setup()
    {
        mySprite = TafeSAMap.GetComponent<SpriteRenderer>().sprite;
        myTexture = mySprite.texture;


        instance = this;
        //player.GetComponent<Unit>().map = this;


        
        p = player.GetComponent<Player>();
        setPlayerPos(1, 1);
        currLocationx = Mathf.RoundToInt(player.transform.position.x);
        currLocationy = Mathf.RoundToInt(player.transform.position.y);
        p.xPosRounded = currLocationx;
        p.yPosRounded = currLocationy;
        p.map = this;


        p.node = graph[p.xPosRounded, p.yPosRounded];



        p.DestinationX = Mathf.RoundToInt(end.transform.position.x);
        p.DestinationY = Mathf.RoundToInt(end.transform.position.y);

        Debug.Log("working");
        Debug.Log(UnitCanEnterTile(34, 44));

        player.GetComponent<Player>().calcPath();

        drawPath = GetComponent<LineRenderer>();
        int currentPathCount = (p.currentPath.Count);
        drawPath.positionCount = currentPathCount;
        Debug.Log("currentPathCount= " + currentPathCount);
        for (int i = 0; i < currentPathCount; i++)
        {
            drawPath.SetPosition(i, new Vector3(p.currentPath[i].x, player.GetComponent<Player>().currentPath[i].y, 0));
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
                if (System.Math.Round(tt.tileVisualPrefab.GetComponent<SpriteRenderer>().color.r, 3) <= 0.790)
                {
                    tiles[x, y] = 1;

                }
            }
        }
       

        // Let's make a u-shaped mountain range


    }

    void GenerateMapVisual()
    {

        float pixel2units = mySprite.rect.width / mySprite.bounds.size.x;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
            
                TileType tt = tileTypes[tiles[x, y]];
                if (!tt.isWalkable)
                {
                    GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);
                    go.transform.parent = gameObject.transform;
                }


            }
        }
        
    }

   








    public int TileIsPassable(int x, int y)
    {
      
            return tiles[x, y];
        
    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY) {

        float cost = 001f;

        if (sourceX != targetX && sourceY != targetY)
        {
            // We are moving diagonally!  Fudge the cost for tie-breaking
            // Purely a cosmetic thing!
            cost += 0.001f;
        }
        if (TileIsPassable(sourceX, sourceY) == 1)
        {
            // We are moving diagonally!  Fudge the cost for tie-breaking
            // Purely a cosmetic thing!
            cost = 999999f;
        }
        return cost;
    }






    void setPlayerPos(int x, int y)
    {


    }

    public void GeneratePathfindingGraph ()
		{
        
			// Initialize the array
			graph = new Node[columns, rows];

			// Initialize a Node for each spot in the array
			for (int x = 0; x < columns; x++) {
				for (int y = 0; y < rows; y++) {
					graph [x, y] = new Node ();
					graph [x, y].x = x;
					graph [x, y].y = y;
				}
			}

			// Now that all the nodes exist, calculate their neighbours
			for (int x = 0; x < columns; x++) {
				for (int y = 0; y < rows; y++) {

					// This is the 4-way connection version:
				/*
									if(x > 0)
					graph[x,y].neighbours.Add( graph[x-1, y] );
				if(x < columns-1)
					graph[x,y].neighbours.Add( graph[x+1, y] );
				if(y > 0)
					graph[x,y].neighbours.Add( graph[x, y-1] );
				if(y < rows-1)
					graph[x,y].neighbours.Add( graph[x, y+1] );
*/

					// This is the 8-way connection version (allows diagonal movement)
					// Try left

					if (x > 0) {
						graph [x, y].neighbours.Add (graph [x - 1, y]);
						if (y > 0)
							graph [x, y].neighbours.Add (graph [x - 1, y - 1]);
						if (y < rows - 1)
							graph [x, y].neighbours.Add (graph [x - 1, y + 1]);
					}

					// Try Right
					if (x < columns - 1) {
						graph [x, y].neighbours.Add (graph [x + 1, y]);
						if (y > 0)
							graph [x, y].neighbours.Add (graph [x + 1, y - 1]);
						if (y < rows - 1)
							graph [x, y].neighbours.Add (graph [x + 1, y + 1]);
					}

					// Try straight up and down
					if (y > 0)
						graph [x, y].neighbours.Add (graph [x, y - 1]);
					if (y < rows - 1)
						graph [x, y].neighbours.Add (graph [x, y + 1]);



					// This also works with 6-way hexes and n-way variable areas (like EU4)
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

    public void GeneratePathTo(int x, int y, GameObject Object){

        //player.GetComponent<Path> ().currentPath = null;
        if (UnitCanEnterTile(x, y) == false)
        {
            // We probably clicked on a mountain or something, so just quit out.
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float> ();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node> ();

		// Setup the "Q" -- the list of nodes we haven't checked yet.
		List<Node> unvisited = new List<Node> ();

        Node source = graph [
			(int)player.GetComponent<Player>().xPosRounded, 
			(int)player.GetComponent<Player>().yPosRounded
                      //(int)Object.transform.position.x,
                      //(int)Object.transform.position.y

                      ];
		Node target = graph [
			              x, 
			              y
		              ];

		dist [source] = 0;
		prev [source] = null;

		foreach (Node v in graph) {
			if (v != source) {
				dist [v] = Mathf.Infinity;
				prev [v] = null;
			}

			unvisited.Add (v);
		}
		while (unvisited.Count > 0) {
			// "u" is going to be the unvisited node with the smallest distance.
			Node u = null;

			foreach (Node possibleU in unvisited) {
				if (u == null || dist [possibleU] < dist [u]) {
					u = possibleU;
				}
			}

			if (u == target) {
				break;	// Exit the while loop!
			}

			unvisited.Remove (u);

			foreach (Node v in u.neighbours) {
				//float alt = dist[u] + u.DistanceTo(v);
				float alt = dist [u] + CostToEnterTile (u.x, u.y, v.x, v.y);
				if (alt < dist [v]) {
					dist [v] = alt;
					prev [v] = u;
				}
			}
		}

		// If we get there, the either we found the shortest route
		// to our target, or there is no route at ALL to our target.

		if (prev [target] == null) {
            // No route between our target and the source
            Debug.Log("No route between our target and the source");
			return;
		}
		List<Node> currentPath = new List<Node> ();

		Node curr = target;

		// Step through the "prev" chain and add it to our path
		while (curr != null) {
			currentPath.Add (curr);
			curr = prev [curr];
		}

		// Right now, currentPath describes a route from out target to our source
		// So we need to invert it!

		currentPath.Reverse ();
        Object.GetComponent<Player>().currentPath = currentPath;
        Debug.Log(currentPath.Count);


        for (int i = 0; i < currentPath.Count; i++)
        {
            //Debug.Log(currentPath[i].x + " - " + currentPath[i].y);
         //   Debug.DrawLine(new Vector3(currentPath[i].x, currentPath[i + 1].y, -1), new Vector3(currentPath[i + 1].x, currentPath[i + 1].y, -1), Color.green);
        }
        //GameObject.FindGameObjectWithTag ("Player").GetComponent<Unit> ().currentPath = currentPath;

        //player.GetComponent<Unit> ().currentPath = currentPath;

    }


	}

