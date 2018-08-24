using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class FloorNavigationController : MonoBehaviour
{

    public Button mapUp;
    public Button mapDown;
    public Button hamburger;
    public Button hamburgerClose;

    public Button createPath;
    public Button generatePath;
    public Button settings;


    public Button back_path;
    public Button back_settings;

    public GameObject GridManager;

    public GameObject Map_nosign;
    public GameObject Map_signed;


    public BoardCreator BoardCreator;
    public int mapFloor;

    public SpriteRenderer spriteNoSign;
    public SpriteRenderer spriteSigned;
    public Sprite[] sprites_nosign;
    public Sprite[] sprites_signed;
    public Text floorTxt;
    public Text menu_text;

    public DropDown DropDownScript;
    public int DestinationFloor;
    public int PlayerFloor;

    public GameObject Hamburger_menu;
    public GameObject createPath_menu;
    public GameObject Settings_menu;
    bool Generated = false;
    
    void Start()
    {

        ResetGeneratedBools();

        BoardCreator = GetComponentInChildren<BoardCreator>();
        mapFloor = BoardCreator.FloorNum;
        GlobalControl.FloorLevel = GlobalControl.Floors.G;
        Hamburger_menu.SetActive(false);
        createPath_menu.SetActive(false);
        Settings_menu.SetActive(false);

        spriteNoSign = Map_nosign.GetComponent<SpriteRenderer>();
        spriteSigned = Map_signed.GetComponent<SpriteRenderer>();

        mapUp.onClick.AddListener(TaskOnClickUp);
        mapDown.onClick.AddListener(TaskOnClickDown);
        

        hamburger.onClick.AddListener(TaskOnClickHamburger);
        hamburgerClose.onClick.AddListener(TaskOnClickHamburgerClose);

  
        generatePath.onClick.AddListener(TaskOnClickGenerate);
        settings.onClick.AddListener(TaskOnClickSettingsMenu);





        floorTxt.text = "Ground Floor";
    }

    public void GeneratePath()
    {
        ResetGeneratedBools();
        DestinationFloor = DropDownScript.FloorB_dropd.value;
        PlayerFloor = DropDownScript.FloorA_dropd.value;
        DetermineFloorsToGenerate(PlayerFloor, DestinationFloor);

        Debug.Log("Floor B bool: " + GlobalControl.FloorBGenerated + "Floor G bool: " + GlobalControl.FloorGGenerated + "Floor 1 bool: " + GlobalControl.Floor1Generated + "Floor 2 bool: " + GlobalControl.Floor2Generated + "Floor 3 bool: " + GlobalControl.Floor3Generated + "Floor 4 bool: " + GlobalControl.Floor4Generated);

        GridManager.GetComponent<BoardCreator>().CreatePath();


        if (Generated == false)
        {
            GridManager.GetComponent<BoardCreator>().GenerateMapVisual();
            Generated = true;

        }

        DropDownScript.AlterDirectionText();
    }


    void TaskOnClickGenerate()
    {
        BoardCreator.ResetAllPaths();
        ResetGeneratedBools();
        GeneratePath();
    }

    void TaskOnClickSettingsMenu()
    {
        if (Settings_menu.activeSelf == false)
        {
             Settings_menu.SetActive(true);
        }
        else
        {
            Settings_menu.SetActive(false);
        }

     

    }

    void DetermineFloorsToGenerate(int PlayerFloor, int DestinationFloor)
    {
        
        if (PlayerFloor < DestinationFloor)
        {
          

            for (int i = PlayerFloor; i < DestinationFloor + 1 ; i++)
            {
                TurnTrueFloor(i);
            }
        }


        if (DestinationFloor < PlayerFloor)
        {


            for (int i = DestinationFloor; i < PlayerFloor + 1; i++)
            {
                TurnTrueFloor(i);
            }
        }



    }


    void TurnTrueFloor(int FloorNum)
    {
        switch (FloorNum)
        {
            case 0:
                GlobalControl.FloorBGenerated = true;
                break;
            case 1:
                GlobalControl.FloorGGenerated = true;
                break;
            case 2:
                GlobalControl.Floor1Generated = true;
                break;
            case 3:
                GlobalControl.Floor2Generated = true;
                break;
            case 4:
                GlobalControl.Floor3Generated = true;
                break;
            case 5:
                GlobalControl.Floor4Generated = true;
                break;

        }
    }
    public bool DoesFloorNeedGeneration(int FloorNum)
    {
        switch (FloorNum)
        {
            case 0:
                return GlobalControl.FloorBGenerated;
            
            case 1:
                return GlobalControl.FloorGGenerated;
              
            case 2:
                return GlobalControl.Floor1Generated;
               
            case 3:
                return GlobalControl.Floor2Generated;
               
            case 4:
                return GlobalControl.Floor3Generated;
               
            case 5:
               
                return GlobalControl.Floor4Generated;
        }
        return false;
    }



    void TaskOnClickBackPath()
    {
        createPath_menu.SetActive(false);
        menu_text.text = "Menu";
    }
    void TaskOnClickBackSettings()
    {
        Settings_menu.SetActive(false);
        menu_text.text = "Menu";
    }

    void TaskOnClickUp()
    {
        //No floors above 5
        if (mapFloor == 5)
        {
            return;
            
        }

        mapFloor++;

        //Change map sprite to new floor sprite
        spriteNoSign.sprite = sprites_nosign[mapFloor];
        spriteSigned.sprite = sprites_signed[mapFloor];

        FloorText();

        //Update Pointer poisitions to class path

        if (DropDownScript.FloorA_dropd.value != 6 && DropDownScript.FloorB_dropd.value != 6)
        {
            DropDownScript.ChangeStartpos();
            DropDownScript.ChangeEndpos();
        }

        if (DoesFloorNeedGeneration(mapFloor) && BoardCreator.drawPath.positionCount == 0)
        {
            //Generate Path for the floors that need generating
            GeneratePath();
           
        }

      

        DropDownScript.AlterDirectionText();

        if (DoesFloorNeedGeneration(mapFloor) == false)
        {
            //Reset Pointers to position off map
            BoardCreator.ResetPointers();
            //Hide Directions on unused floors
            DropDownScript.DirectionsTxt.text = "";
        }
    }

    void TaskOnClickCreatePath()
    {
        //Show path creator interface
        createPath_menu.SetActive(true);
        menu_text.text = "Path Creator";
    }

    void TaskOnClickDown()
    {

        if (mapFloor == 0)
        {
            return;

        }

            mapFloor--;
            spriteNoSign.sprite = sprites_nosign[mapFloor];
            spriteSigned.sprite = sprites_signed[mapFloor];
            FloorText();
        if (DropDownScript.FloorA_dropd.value != 6 && DropDownScript.FloorB_dropd.value != 6)
        {
            DropDownScript.ChangeStartpos();
            DropDownScript.ChangeEndpos();
        }

        if (DoesFloorNeedGeneration(mapFloor) && BoardCreator.drawPath.positionCount == 0)
        {
            GeneratePath();
        }
        DropDownScript.AlterDirectionText();
        if (DoesFloorNeedGeneration(mapFloor) == false)
        {
            //Reset Pointers to position off map
            BoardCreator.ResetPointers();
            //Hide Directions on unused floors
            DropDownScript.DirectionsTxt.text = "";
        }
    }

    void TaskOnClickHamburger()
    {
     Hamburger_menu.SetActive(true);
     // menu_text.text = "Menu";

        createPath_menu.SetActive(true);


    }
    void TaskOnClickHamburgerClose()
    {
     
        Hamburger_menu.SetActive(false);
        createPath_menu.SetActive(false);
 

    }


    void ResetGeneratedBools()
    {
        GlobalControl.FloorBGenerated = false;
        GlobalControl.FloorGGenerated = false;
        GlobalControl.Floor1Generated = false;
        GlobalControl.Floor2Generated = false;
        GlobalControl.Floor3Generated = false;
        GlobalControl.Floor4Generated = false;
    }

    void FloorText()
    {
        switch (mapFloor)
        {
            case 5:
                floorTxt.text = "4F";
                GlobalControl.FloorLevel = GlobalControl.Floors.F4;
                break;
            case 4:
                floorTxt.text = "3F";
                GlobalControl.FloorLevel = GlobalControl.Floors.F3;
                break;
            case 3:
                floorTxt.text = "2F";
                GlobalControl.FloorLevel = GlobalControl.Floors.F2;
                break;
            case 2:
                floorTxt.text = "1F";
                GlobalControl.FloorLevel = GlobalControl.Floors.F1;
                break;
            case 1:
                floorTxt.text = "Ground Floor";
                GlobalControl.FloorLevel = GlobalControl.Floors.G;
                break;
            default:
                floorTxt.text = "Basement";
                GlobalControl.FloorLevel = GlobalControl.Floors.B;
                break;
        }
    }
}