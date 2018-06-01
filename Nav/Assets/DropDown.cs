
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class DropDown : MonoBehaviour {

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


    List<string> basement_DropOptions = new List<string> { };
    List<string> ground_DropOptions = new List<string> { };
    List<string> F1_DropOptions = new List<string> { };
    List<string> F2_DropOptions = new List<string> { };
    List<string> F3_DropOptions = new List<string> { };
    List<string> F4_DropOptions = new List<string> { };





    public GameObject Start1;
    public GameObject End1;

    public List<string> GClasses;

    // Use this for initialization
    void Start() {
       // var GClass_var = new[] { "A.G01", "A.G02", "A.G03", "A.G04", "A.G05", "A.G06", "A.G07", "A.G08", "A.G09", "A.G10", "A.G11", "A.G12", "A.G13", "A.G14", "A.G15", "A.G16", "A.G17", "A.G18", "A.G19", "A.G20", "A.G21", "A.G22", "A.G23", "B.G01", "B.G02", "B.G03", "B.G04", "B.G05", "B.G06", "B.G07", "B.G08", "B.G09", "B.G10", "B.G11", "B.G12", "B.G13", "B.G14", "B.G15", "B.G16" };
     //  GClasses.AddRange(GClass_var.Where((s, i) => i < GClass_var.Length).ToList());


        for (int i = 0; i < basementRooms.Count; i ++)
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

     
        FloorA_dropd = FloorA.GetComponent<Dropdown>();
        FloorB_dropd = FloorB.GetComponent<Dropdown>();
        ClassA_dropd = ClassA.GetComponent<Dropdown>();
        ClassB_dropd = ClassB.GetComponent<Dropdown>();
        UpdateDropDowns();

        FloorA_dropd.onValueChanged.AddListener(delegate {
            FloorADropdownValueChanged(FloorA_dropd);
        });

        FloorB_dropd.onValueChanged.AddListener(delegate {
            FloorBDropdownValueChanged(FloorB_dropd);
        });

        ClassA_dropd.onValueChanged.AddListener(delegate {
            ClassADropdownValueChanged(ClassA_dropd);
        });

        ClassB_dropd.onValueChanged.AddListener(delegate {
            ClassBDropdownValueChanged(ClassB_dropd);
        });
        ClassA_dropd.ClearOptions();
        ClassB_dropd.ClearOptions();
        ClassA_dropd.AddOptions(ground_DropOptions);
        ClassB_dropd.AddOptions(ground_DropOptions);

        FloorA_dropd.value = 1;
        FloorB_dropd.value = 1;
    }

    // Update is called once per frame
    void Update() {

    }

    void UpdateDropDowns()
    {



    }
    void FloorADropdownValueChanged(Dropdown change)
    {
        ClassA_dropd.ClearOptions();
        switch (FloorA_dropd.value)
        {
            case 0:
                //Basement
                ClassA_dropd.AddOptions(basement_DropOptions);
                break;
            case 1:
                //Ground Floor
                ClassA_dropd.AddOptions(ground_DropOptions);
                break;
            case 2:
                //1F
                ClassA_dropd.AddOptions(F1_DropOptions);
                break;
            case 3:
                //2F
                ClassA_dropd.AddOptions(F2_DropOptions);
                break;
            case 4:
                //3F
                ClassA_dropd.AddOptions(F3_DropOptions);
                break;
            case 5:
                //4F
                ClassA_dropd.AddOptions(F4_DropOptions);
                break;

        }
    }





    void ClassADropdownValueChanged(Dropdown change)
    {

        switch (FloorA_dropd.value)
        {
            case 0:
                //Basement
                Start1.transform.position = new Vector3(basementRooms[ClassA_dropd.value].x, basementRooms[ClassA_dropd.value].y, 0);
                break;
            case 1:
                //Ground Floor
                Start1.transform.position = new Vector3(groundRooms[ClassA_dropd.value].x, groundRooms[ClassA_dropd.value].y, 0);
                break;
            case 2:
                //1F
                Start1.transform.position = new Vector3(F1rooms[ClassA_dropd.value].x, F1rooms[ClassA_dropd.value].y, 0);
                break;
            case 3:
                //2F
                Start1.transform.position = new Vector3(F2rooms[ClassA_dropd.value].x, F2rooms[ClassA_dropd.value].y, 0);
                break;
            case 4:
                //3F
                Start1.transform.position = new Vector3(F3rooms[ClassA_dropd.value].x, F3rooms[ClassA_dropd.value].y, 0);
                break;
            case 5:
                //4F
                Start1.transform.position = new Vector3(F4rooms[ClassA_dropd.value].x, F4rooms[ClassA_dropd.value].y, 0);
                break;

        }



    }

    void ClassBDropdownValueChanged(Dropdown change)
    {
      
      
        switch (FloorB_dropd.value)
        {
            case 0:
                //Basement
                End1.transform.position = new Vector3(basementRooms[ClassB_dropd.value].x, basementRooms[ClassB_dropd.value].y, 0);
                break;
            case 1:
                //Ground Floor
                End1.transform.position = new Vector3(groundRooms[ClassB_dropd.value].x, groundRooms[ClassB_dropd.value].y, 0);
                break;
            case 2:
                //1F
                End1.transform.position = new Vector3(F1rooms[ClassB_dropd.value].x, F1rooms[ClassB_dropd.value].y, 0);
                break;
            case 3:
                //2F
                End1.transform.position = new Vector3(F2rooms[ClassB_dropd.value].x, F2rooms[ClassB_dropd.value].y, 0);
                break;
            case 4:
                //3F
                End1.transform.position = new Vector3(F3rooms[ClassB_dropd.value].x, F3rooms[ClassB_dropd.value].y, 0);
                break;
            case 5:
                //4F
                End1.transform.position = new Vector3(F4rooms[ClassB_dropd.value].x, F4rooms[ClassB_dropd.value].y, 0);
                break;

        }




    }



    void FloorBDropdownValueChanged(Dropdown change)
    {
        ClassB_dropd.ClearOptions();
      




        switch (FloorB_dropd.value)
        {
            case 0:
                //Basement
                ClassB_dropd.AddOptions(basement_DropOptions);
                break;
            case 1:
                //Ground Floor
                ClassB_dropd.AddOptions(ground_DropOptions);
                break;
            case 2:
                //1F
                ClassB_dropd.AddOptions(F1_DropOptions);
                break;
            case 3:
                //2F
                ClassB_dropd.AddOptions(F2_DropOptions);
                break;
            case 4:
                //3F
                ClassB_dropd.AddOptions(F3_DropOptions);
                break;
            case 5:
                //4F
                ClassB_dropd.AddOptions(F4_DropOptions);
                break;

        }




    }

}
