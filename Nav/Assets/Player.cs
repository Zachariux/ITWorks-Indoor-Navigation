using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float xPos;
    public float yPos;

    public int xPosRounded=0;
    public int yPosRounded=0;


    public Node node;

    public int DestinationX;
    public int DestinationY;


    public List<Node> currentPath = null;
    public BoardCreator map;
    Vector3 startPos;
    Vector3 endPos;

    public void positionRoundToInt()
    {


    }
    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    } 

    public void calcPath()
    {
        map.GeneratePathTo((int)DestinationX, (int)DestinationY,this.gameObject);
        if (currentPath != null)
        {

            int currNode = 0;

            while (currNode < currentPath.Count - 1)
            {

                Vector3 start = TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y) +
                    new Vector3(0, 0, -1f);
                Vector3 end = TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].y) +
                    new Vector3(0, 0, -1f);

                Debug.DrawLine(start, end, Color.blue);

                currNode++;



            }
        }

    }
}
