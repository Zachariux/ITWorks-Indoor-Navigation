using UnityEngine;


[CreateAssetMenu(menuName = "Room")]

public class Room : ScriptableObject
{
    public int x;
    public int y;
    public string roomName;
    
    public enum Floor {B,G,F1,F2,F3,F4};
    public Floor floor;

    public enum Lift {A,B,C,D};
    public Lift lift;


}

