
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
    public LineRenderer drawnPath;
    public List<Lifts> lifts = new List<Lifts>();
    public Text DirectionsTxt;

    public Lifts DestinationLift;
    List<string> basement_DropOptions = new List<string> { };
    List<string> ground_DropOptions = new List<string> { };
    List<string> F1_DropOptions = new List<string> { };
    List<string> F2_DropOptions = new List<string> { };
    List<string> F3_DropOptions = new List<string> { };
    List<string> F4_DropOptions = new List<string> { };

    List<string> FloorOptions = new List<string> {"B", "G","1F","2F","3F","4F","?" };



    List<string> starting = new List<string> { };

    public List<List<string>> allOptions = new List<List<string>> {};
    public List<List<Room>> allRooms = new List<List<Room>> { };


    public enum Directions { Down = 0, SameFloor = 1, Up = 2 };
    public Directions Direction;

    Vector3 prevStartPos = new Vector3();

    Vector3 prevEndPos = new Vector3();

    //RED
    public GameObject Start1;

    //BLUE
    public GameObject End1;


    // Use this for initialization
    void Start()
    {

    

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
            ClassBDropdownValueChanged(ClassB_dropd);
        });

   

        //DropDown option selected when panel opens:
    

        
        starting.Add("Select Floor");

        FloorA_dropd.options.Clear();
        FloorA_dropd.AddOptions(FloorOptions);

        FloorB_dropd.options.Clear();
        FloorB_dropd.AddOptions(FloorOptions);



        FloorA_dropd.value = 6;
        FloorB_dropd.value = 6;

        FloorA_dropd.options.RemoveAt(FloorA_dropd.options.Count - 1);
        FloorB_dropd.options.RemoveAt(FloorB_dropd.options.Count - 1);

        ClassA_dropd.AddOptions(starting);
        ClassA_dropd.options.RemoveAt(ClassA_dropd.options.Count - 1);

        ClassB_dropd.AddOptions(starting);
        ClassB_dropd.options.RemoveAt(ClassB_dropd.options.Count - 1);


    }

   public void ResetDropDowns()
    {

        ClassA_dropd.options.Clear();

        ClassB_dropd.options.Clear();

        FloorA_dropd.options.Clear();
        
        FloorB_dropd.options.Clear();

        FloorA_dropd.options.Clear();
        FloorA_dropd.AddOptions(FloorOptions);

        FloorB_dropd.options.Clear();
        FloorB_dropd.AddOptions(FloorOptions);



        FloorA_dropd.value = 6;
        FloorB_dropd.value = 6;

        FloorA_dropd.options.RemoveAt(FloorA_dropd.options.Count - 1);
        FloorB_dropd.options.RemoveAt(FloorB_dropd.options.Count - 1);

        ClassA_dropd.AddOptions(starting);
        ClassA_dropd.options.RemoveAt(ClassA_dropd.options.Count - 1);

        ClassB_dropd.AddOptions(starting);
        ClassB_dropd.options.RemoveAt(ClassB_dropd.options.Count - 1);

    }













    void FindLiftA(Room r)
    {

        for (int i = 0; i < lifts.Count; i++)
        {
            //Specific Basement Case:
            if (GlobalControl.FloorNum == 0)
            {
                //Specific Elavator Usecases
                //Basement Player on B side

                if (ClassB_dropd.value < 4)
                {
                    Start1.transform.position = new Vector3(lifts[1].x, lifts[1].y, 0);
                }

                //Basement Player on D side:
                if (ClassB_dropd.value >= 4)
                {
                    Start1.transform.position = new Vector3(lifts[2].x, lifts[2].y, 0);
                }
            }


            //Specific 2F Case
            if(GlobalControl.FloorNum == 3)
            {
                //Specific Elavator Usecases
                //Basement Player on A side

                if (ClassB_dropd.value < 23)
                {
                    Start1.transform.position = new Vector3(lifts[0].x, lifts[0].y, 0);
                }

                //Basement Player on D side:
                if (ClassB_dropd.value >= 23)
                {
                    Start1.transform.position = new Vector3(lifts[2].x, lifts[2].y, 0);
                }


            }



            if (lifts[i].lift.ToString() == r.lift.ToString())
            {


                if (lifts[i].floorAccess[GlobalControl.FloorNum])
                {

                    Start1.transform.position = new Vector3(lifts[i].x, lifts[i].y, 0);
                    prevStartPos = new Vector3(lifts[i].x, lifts[i].y, 0);
                    DestinationLift = lifts[i];

                    return;
                }
                else
                {



                    return;
                }

            }
        }
    }
    public void ChangeStartpos()
    {

      

        GlobalControl.Floors globalFloorNum = (GlobalControl.Floors)Enum.Parse(typeof(GlobalControl.Floors), GlobalControl.FloorLevel.ToString());
        int floorInt = (int)globalFloorNum;
     

        for (int i = 0; i < 5; i++)
        {
            if (FloorA_dropd.value == i && i == floorInt)
            {
                Start1.transform.position = new Vector3(allRooms[floorInt][ClassA_dropd.value].x, allRooms[floorInt][ClassA_dropd.value].y, 0);
                return;
            }

        }

        FindLiftA(allRooms[FloorA_dropd.value][ClassA_dropd.value]);
      




    }

    void FindLiftB(Room r)
    {

        for (int i = 0; i < lifts.Count; i++)
        {
            //Specific Basement Case:
            if(GlobalControl.FloorNum == 0)
            {
                //Specific Elavator Usecases
                //Basement Player on B side:



                if (ClassA_dropd.value < 4)
                {
                    End1.transform.position = new Vector3(lifts[1].x, lifts[1].y, 0);
                    DestinationLift = lifts[1];
                }

                //Basement Player on D side:
                if (ClassA_dropd.value >= 4)
                {
                    End1.transform.position = new Vector3(lifts[2].x, lifts[2].y, 0);
                    DestinationLift = lifts[2];
                }
                return;
            }

            //Specific 2F Case
            if (GlobalControl.FloorNum == 3)
            {
                //Specific Elavator Usecases
                //Basement Player on A side

                if (ClassA_dropd.value < 23)
                {
                    End1.transform.position = new Vector3(lifts[0].x, lifts[0].y, 0);
                    DestinationLift = lifts[0];
                }

                //Basement Player on D side:
                if (ClassA_dropd.value >= 23)
                {
                    End1.transform.position = new Vector3(lifts[2].x + 3, lifts[2].y+3, 0);
                    DestinationLift = lifts[2];
                }
                return;


            }

            if (lifts[i].lift.ToString() == r.lift.ToString())
            {


                if (lifts[i].floorAccess[GlobalControl.FloorNum])
                {
                   
                    End1.transform.position = new Vector3(lifts[i].x, lifts[i].y, 0);
                    prevEndPos = new Vector3(lifts[i].x, lifts[i].y, 0);
                    DestinationLift = lifts[i];
                   
                    return;
                }
                else
                {
                    


                    return;
                }

            }
        }
    }

    public void AlterDirectionText()
    {

        if(TravelDirection(DestinationLift) == Directions.SameFloor.ToString())
        {
            DirectionsTxt.text = "You are on the destination floor. Follow the path shown.";
            return;
        }
        if (DestinationLift != null)
        {
            DirectionsTxt.text = "Take the " + DestinationLift.name + " " + TravelDirection(DestinationLift);
        }
    }


    public string TravelDirection(Lifts DestinationLift)
    {
    

        if (GlobalControl.FloorNum > FloorB_dropd.value)
        {
            Direction = Directions.Down;
        }
        if (GlobalControl.FloorNum < FloorB_dropd.value)
        {
            Direction = Directions.Up;
        }
        if (GlobalControl.FloorNum == FloorB_dropd.value)
        {
            Direction = Directions.SameFloor;
        }
        return Direction.ToString();
    }


    public bool LiftOnFloor(Room r)
    {
        for (int i = 0; i < lifts.Count; i++)
        {
            if (lifts[i].lift.ToString() == r.lift.ToString())
            {
                switch (GlobalControl.FloorLevel)
                {
                    case GlobalControl.Floors.B:
                        //Basement
                        if (lifts[i].BFloorAccess == true)
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



    public void ChangeEndpos()
    {


        GlobalControl.Floors globalFloorNum = (GlobalControl.Floors)Enum.Parse(typeof(GlobalControl.Floors), GlobalControl.FloorLevel.ToString());
        int floorInt = (int)globalFloorNum;


        for (int i = 0; i < 5; i++)
        {
            if (FloorB_dropd.value == i && i == floorInt)
            {

                End1.transform.position = new Vector3(allRooms[floorInt][ClassB_dropd.value].x, allRooms[floorInt][ClassB_dropd.value].y, 0);
                return;
            }
            if (i == 4)
            {
                FindLiftB(allRooms[FloorB_dropd.value][ClassB_dropd.value]);




            }
        }

    }
    void ClassBDropdownValueChanged(Dropdown change)
    {
        if (drawnPath != null)
        {
            drawnPath.positionCount = 0;
        }
        ChangeEndpos();
    }


    void ClassADropdownValueChanged(Dropdown change)
    {
        if (drawnPath != null)
        {
            drawnPath.positionCount = 0;
        }

        ChangeStartpos();
    }

    


    void FloorADropdownValueChanged(Dropdown change)
    {

        ClassA_dropd.ClearOptions();

        if (FloorA_dropd.value == 6)
        {
            return;
        }

        for (int i = 0; i < 6; i++)
        {
            if (FloorA_dropd.value == i)
            {
                ClassA_dropd.AddOptions(allOptions[i]);
                ClassA_dropd.value = ClassA_dropd.options.Count;
                ClassA_dropd.options.RemoveAt(ClassA_dropd.options.Count - 1);
              
                return;
            }
        }



    }



    void FloorBDropdownValueChanged(Dropdown change)
    {
        ClassB_dropd.ClearOptions();
        if (FloorB_dropd.value == 6)
        {
            return;
        }

        for (int i = 0; i < 6; i++)
        {
            if (FloorB_dropd.value == i)
            {
                ClassB_dropd.AddOptions(allOptions[i]);
                ClassB_dropd.value = ClassB_dropd.options.Count;
                ClassB_dropd.options.RemoveAt(ClassB_dropd.options.Count -1);
                return;
            }
        }




    }

}
