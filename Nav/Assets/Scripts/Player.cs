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

    public int DestxPosRounded = 0;
    public int DestyPosRounded = 0;

    public List<Node> currentPath;
  


    public void positionRoundToInt()
    {


    }
    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    } 

   
}
