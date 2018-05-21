
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

    public List<room> rooms = new List<room>();

    
    List<string> m_DropOptions = new List<string> { };


    


    public GameObject Start1;
    public GameObject End1;

    public List<string> GClasses;

    // Use this for initialization
    void Start() {
       // var GClass_var = new[] { "A.G01", "A.G02", "A.G03", "A.G04", "A.G05", "A.G06", "A.G07", "A.G08", "A.G09", "A.G10", "A.G11", "A.G12", "A.G13", "A.G14", "A.G15", "A.G16", "A.G17", "A.G18", "A.G19", "A.G20", "A.G21", "A.G22", "A.G23", "B.G01", "B.G02", "B.G03", "B.G04", "B.G05", "B.G06", "B.G07", "B.G08", "B.G09", "B.G10", "B.G11", "B.G12", "B.G13", "B.G14", "B.G15", "B.G16" };
     //  GClasses.AddRange(GClass_var.Where((s, i) => i < GClass_var.Length).ToList());


        for (int i = 0; i < rooms.Count; i ++)
        {
            m_DropOptions.Add(rooms[i].name);
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
        if (FloorA_dropd.value == 1)
        {
            ClassA_dropd.AddOptions(m_DropOptions);
        }
    }





    void ClassADropdownValueChanged(Dropdown change)
    {
      
        Start1.transform.position = new Vector3(rooms[ClassA_dropd.value].x, rooms[ClassA_dropd.value].y, 0);





    }

    void ClassBDropdownValueChanged(Dropdown change)
    {
      
        End1.transform.position = new Vector3(rooms[ClassB_dropd.value].x, rooms[ClassB_dropd.value].y, 0);





    }



    void FloorBDropdownValueChanged(Dropdown change)
    {
        ClassB_dropd.ClearOptions();
        if (FloorB_dropd.value == 1)
        {
            ClassB_dropd.AddOptions(m_DropOptions);
        }
    }

}
