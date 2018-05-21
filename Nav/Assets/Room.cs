using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "room")]

public class Room : ScriptableObject
{
    public int x;
    public int y;
    public string name;
}