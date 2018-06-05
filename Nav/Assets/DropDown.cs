
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
        // var GClass_var = new[] { "A.G01", "A.G02", "A.G03", "A.G04", "A.G05", "A.G06", "A.G07", "A.G08", "A.G09", "A.G10", "A.G11", "A.G12", "A.G13", "A.G14", "A.G15", "A.G16", "A.G17", "A.G18", "A.G19", "A.G20", "A.G21", "A.G22", "A.G23", "B.G01", "B.G02", "B.G03", "B.G04", "B.G05", "B.G06", "B.G07", "B.G08", "B.G09", "B.G10", "B.G11", "B.G12", "B.G13", "B.G14", "B.G15", "B.G16" };
        //  GClasses.AddRange(GClass_var.Where((s, i) => i < GClass_var.Length).ToList());


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
        UpdateDropDowns();

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
            ClassBDropdownValueChanged(ClassB_dropd);
        });
        ClassA_dropd.ClearOptions();
        ClassB_dropd.ClearOptions();
        ClassA_dropd.AddOptions(ground_DropOptions);
        ClassB_dropd.AddOptions(ground_DropOptions);

        FloorA_dropd.value = 1;
        FloorB_dropd.value = 1;


        Start1.transform.position = new Vector3(allRooms[1][ClassA_dropd.value].x, allRooms[1][ClassA_dropd.value].y, 0);
        End1.transform.position = new Vector3(allRooms[1][ClassA_dropd.value].x, allRooms[1][ClassA_dropd.value].y, 0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateDropDowns()
    {



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


        changeStartpos();
    }





    void ClassADropdownValueChanged(Dropdown change)
    {
        changeStartpos();
    }






    void FindLiftA(Room r)
    {
        GlobalControl.Floors globalFloorNum = (GlobalControl.Floors)Enum.Parse(typeof(GlobalControl.Floors), GlobalControl.FloorLevel.ToString());
        int floorInt = (int)globalFloorNum;
        for (int i = 0; i < lifts.Count; i++)
        {
            if (lifts[i].lift.ToString() == r.lift.ToString())
            {

                Debug.Log(lifts[i].lift);
                Debug.Log(r.lift.ToString());

                if (lifts[i].floorAccess[floorInt])
                {

                    Start1.transform.position = new Vector3(lifts[i].x, lifts[i].y, 0);
                    prevStartPos = new Vector3(lifts[i].x, lifts[i].y, 0);
                    return;
                }
                else
                {
                    Start1.transform.position = prevEndPos;
                    return;
                }

            }
        }
    }
    public void changeStartpos()
    {

        Debug.Log("Test changeStartpos");

        GlobalControl.Floors globalFloorNum = (GlobalControl.Floors)Enum.Parse(typeof(GlobalControl.Floors), GlobalControl.FloorLevel.ToString());
        int floorInt = (int)globalFloorNum;
        Debug.Log(floorInt);

        for (int i = 0; i < 5; i++)
        {
            if (FloorA_dropd.value == i && i == floorInt)
            {
                Debug.Log("Test changeStartpos1");
                Start1.transform.position = new Vector3(allRooms[floorInt][ClassA_dropd.value].x, allRooms[floorInt][ClassA_dropd.value].y, 0);
                return;
            }
           
        }
        
            FindLiftA(allRooms[FloorA_dropd.value][ClassA_dropd.value]);
            Debug.Log("Test changeStartpos2");
            Debug.Log(allRooms[FloorA_dropd.value][ClassA_dropd.value].name);

        


    }

    void FindLiftB(Room r)
    {


                GlobalControl.Floors globalFloorNum = (GlobalControl.Floors)Enum.Parse(typeof(GlobalControl.Floors), GlobalControl.FloorLevel.ToString());
                int floorInt = (int)globalFloorNum;
        for (int i = 0; i < lifts.Count; i++)
        {
            if (lifts[i].lift.ToString() == r.lift.ToString())
            {

                Debug.Log(lifts[i].lift);
                Debug.Log(r.lift.ToString());

                if (lifts[i].floorAccess[floorInt])
                {

                    End1.transform.position = new Vector3(lifts[i].x, lifts[i].y, 0);
                    prevEndPos = new Vector3(lifts[i].x, lifts[i].y, 0);
                    return;
                }
                else
                {

                    End1.transform.position = prevStartPos;
                    return;
                }
                
            }
        }
    }

    public bool liftOnFloor(Room r)
    {
        for (int i = 0; i < lifts.Count; i++)
        {
            if (lifts[i].lift.ToString() == r.lift.ToString())
            {
                switch (GlobalControl.FloorLevel)
                {
                    case GlobalControl.Floors.B:
                        //Basement
                        if(lifts[i].BFloorAccess == true)
                        {
                            return true;
                        }
                        break;
                    case GlobalControl.Floors.G:
                        //Basement
                        if (lifts[i].GFloorAccess == true)
                        {
                            return true;
                        }
                        break;
                    case GlobalControl.Floors.F1:
                        //Basement
                        if (lifts[i].F1FloorAccess == true)
                        {
                            return true;
                        }
                        break;
                    case GlobalControl.Floors.F2:
                        //Basement
                        if (lifts[i].F2loorAccess == true)
                        {
                            return true;
                        }
                        break;
                    case GlobalControl.Floors.F3:
                        //Basement
                        if (lifts[i].F3loorAccess == true)
                        {
                            return true;
                        }
                        break;
                    case GlobalControl.Floors.F4:
                        //Basement
                        if (lifts[i].F4loorAccess == true)
                        {
                            return true;
                        }
                        break;
                }

              
            }
        }
        return false;
    }



    public void changeEndpos()
    {
           Debug.Log("Test changeEndpos");

        GlobalControl.Floors globalFloorNum = (GlobalControl.Floors)Enum.Parse(typeof(GlobalControl.Floors), GlobalControl.FloorLevel.ToString());
        int floorInt = (int)globalFloorNum;
        Debug.Log(floorInt);

        for (int i = 0; i < 5; i++)
        {
            if (FloorB_dropd.value == i && i == floorInt)
            {
                Debug.Log("Test changeEndpos1");
                End1.transform.position = new Vector3(allRooms[floorInt][ClassB_dropd.value].x, allRooms[floorInt][ClassB_dropd.value].y, 0);
                return;
            }
            if (i == 4) 
            {
                FindLiftB(allRooms[FloorB_dropd.value][ClassB_dropd.value]);


                Debug.Log("Test changeEndpos2");
                
            }
        }
      
    }
    void ClassBDropdownValueChanged(Dropdown change)
    {

        changeEndpos();
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

        changeEndpos();


    }

}
