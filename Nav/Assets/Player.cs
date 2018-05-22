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

    public List<Node> currentPath;
    public List<Node> currentPathB;
    public List<Node> currentPathG;
    public List<Node> currentPath1F;
    public List<Node> currentPath2F;
    public List<Node> currentPath3F;
    public List<Node> currentPath4F;

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

   
}
