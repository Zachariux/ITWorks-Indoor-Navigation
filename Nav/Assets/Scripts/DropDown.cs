
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class DropDown : MonoBehaviour
{

    public GameObject FloorA;
    public GameObject FloorB;
    public GameObject ClassA;
    public GameObject ClassB;

    public Dropdown FloorA_dropd;
    public Dropdown FloorB_dropd;
    public Dropdown ClassA_dropd;
    public Dropdown ClassB_dropd;

    public List<Room> basementRooms = new List<Room>();
    public List<Room> groundRooms = new List<Room>();
    public List<Room> F1rooms = new List<Room>();
    public List<Room> F2rooms = new List<Room>();
    public List<Room> F3rooms = new List<Room>();
    public List<Room> F4rooms = new List<Room>();

    public List<Lifts> lifts = new List<Lifts>();

    List<string> basement_DropOptions = new List<string> { };
    List<string> ground_DropOptions = new List<string> { };
    List<string> F1_DropOptions = new List<string> { };
    List<string> F2_DropOptions = new List<string> { };
    List<string> F3_DropOptions = new List<string> { };
    List<string> F4_DropOptions = new List<string> { };

    public List<List<string>> allOptions = new List<List<string>> {};
    public List<List<Room>> allRooms = new List<List<Room>> { };


    Vector3 prevStartPos = new Vector3();

    Vector3 prevEndPos = new Vector3();

    //RED
    public GameObject Start1;

    //BLUE
    public GameObject End1;

    public List<string> GClasses;

    // Use this for initialization
    void Start()
    {
    
        Debug.Log(lifts[0].lift.ToString());
        Debug.Log(basementRooms[0].lift.ToString());
        for (int i = 0; i < basementRooms.Count; i++)
        {
            basement_DropOptions.Add(basementRooms[i].name);
        }

        for (int i = 0; i < groundRooms.Count; i++)
        {
            ground_DropOptions.Add(groundRooms[i].name);
        }

        for (int i = 0; i < F1rooms.Count; i++)
        {
            F1_DropOptions.Add(F1rooms[i].name);
        }

        for (int i = 0; i < F2rooms.Count; i++)
        {
            F2_DropOptions.Add(F2rooms[i].name);
        }

        for (int i = 0; i < F3rooms.Count; i++)
        {
            F3_DropOptions.Add(F3rooms[i].name);
        }
        for (int i = 0; i < F4rooms.Count; i++)
        {
            F4_DropOptions.Add(F4rooms[i].name);
        }

        allOptions.Add(basement_DropOptions);
        allOptions.Add(ground_DropOptions);
        allOptions.Add(F1_DropOptions);
        allOptions.Add(F2_DropOptions);
        allOptions.Add(F3_DropOptions);
        allOptions.Add(F4_DropOptions);

        allRooms.Add(basementRooms);
        allRooms.Add(groundRooms);
        allRooms.Add(F1rooms);
        allRooms.Add(F2rooms);
        allRooms.Add(F3rooms);
        allRooms.Add(F4rooms);

        FloorA_dropd = FloorA.GetComponent<Dropdown>();
        FloorB_dropd = FloorB.GetComponent<Dropdown>();
        ClassA_dropd = ClassA.GetComponent<Dropdown>();
        ClassB_dropd = ClassB.GetComponent<Dropdown>();

        FloorA_dropd.onValueChanged.AddListener(delegate
        {
            FloorADropdownValueChanged(FloorA_dropd);
        });

        FloorB_dropd.onValueChanged.AddListener(delegate
        {
            FloorBDropdownValueChanged(FloorB_dropd);
        });

        ClassA_dropd.onValueChanged.AddListener(delegate
        {
            ClassADropdownValueChanged(ClassA_dropd);
        });

        ClassB_dropd.onValueChanged.AddListener(delegate
        {
            //ClassBDropdownValueChanged(ClassB_dropd);
        });

   

        ClassA_dropd.ClearOptions();
        ClassB_dropd.ClearOptions();
        ClassA_dropd.AddOptions(ground_DropOptions);
        ClassB_dropd.AddOptions(ground_DropOptions);

        FloorA_dropd.value = 50;
        FloorB_dropd.value = 50;

        //DropDown option selected when panel opens:
        ClassA_dropd.value = ClassA_dropd.options.Count;
        ClassB_dropd.value = 0;
      

    }

    void FloorADropdownValueChanged(Dropdown change)
    {
     
        ClassA_dropd.ClearOptions();

        for (int i = 0; i<6; i++)
        {
            if(FloorA_dropd.value == i)
            {
                ClassA_dropd.AddOptions(allOptions[i]);
                return;
            }
        }
    }

    void ClassADropdownValueChanged(Dropdown change)
    {
       
    }
    void FloorBDropdownValueChanged(Dropdown change)
    {
        ClassB_dropd.ClearOptions();


        for (int i = 0; i < 6; i++)
        {
            if (FloorB_dropd.value == i)
            {
                ClassB_dropd.AddOptions(allOptions[i]);
                return;
            }
        }



    }

}
