using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

[CreateAssetMenu(menuName = "Lifts")]
public class Lifts : ScriptableObject {


    public int x;
    public int y;

    public enum Lift { A, B, C, D };
    public Lift lift;

    public bool BFloorAccess = new bool();
    public bool GFloorAccess;
    public bool F1FloorAccess;
    public bool F2loorAccess;
    public bool F3loorAccess;
    public bool F4loorAccess;

    public List<bool> floorAccess = new List<bool> {};
    
}
