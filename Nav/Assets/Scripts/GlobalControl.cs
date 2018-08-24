using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalControl {


    public enum Floors
    { B = 0, G = 1, F1 = 2, F2 = 3, F3 = 4, F4 = 5 }


    public static Floors FloorLevel;

    public static int FloorNum = 1;

    //Floors that need path to be generated
    public static bool FloorBGenerated;
    public static bool FloorGGenerated;
    public static bool Floor1Generated;
    public static bool Floor2Generated;
    public static bool Floor3Generated;
    public static bool Floor4Generated;



}
