using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RoomSearch : MonoBehaviour {

    public List<Room> Rooms = new List<Room>();

    public InputField roomASearch;
    public InputField roomBSearch;

    public GameObject dropDownControlHolder;
    public DropDown dropdwnScript;

    public DropDown roomADrop;
    public DropDown roomBDrop;

    public List<string> k = new List<string>();

    // Use this for initialization
    void Start () {

        dropdwnScript = dropDownControlHolder.GetComponent<DropDown>();

        roomASearch.onValueChanged.AddListener(delegate
        {
            roomASearchValueChanged(roomASearch);
        });

        //roomASearch.on.AddListener(delegate
        //{
        //    roomASearchValueInput(roomAInput);
        //});

        roomBSearch.onValueChanged.AddListener(delegate
        {
            roomBSearchValueChanged(roomBSearch);
        });

    }
	
    void roomASearchValueChanged(InputField aChange)
    {
        //Debug.Log("Testing String");
        try
        {
            string roomAInput = roomASearch.text;
            if (Rooms.FirstOrDefault(item => item.roomName == roomAInput) != null)
            {
                       Room searchTarget = Rooms.FirstOrDefault(item => item.roomName == roomAInput);
                            dropdwnScript.ClassA_dropd.ClearOptions();
                            k.Add(searchTarget.name);
                            dropdwnScript.ClassA_dropd.AddOptions(k);
            }
                    
            
        }
        catch
        {

        }
    }

    void roomASearchValueInput(InputField aInput)
    {

    }

    void roomBSearchValueChanged(InputField bChange)
    {
        //Debug.Log("Testing String");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
