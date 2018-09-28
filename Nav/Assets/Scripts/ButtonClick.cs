using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class floorNavigationController : MonoBehaviour
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


    public Button QRScene_btn;

    public GameObject GridManager;

    public GameObject Map_nosign;
    public GameObject Map_signed;

    public int mapFloor = 1;

    public SpriteRenderer spriteNoSign;
    public SpriteRenderer spriteSigned;
    public Sprite[] sprites_nosign;
    public Sprite[] sprites_signed;
    public Text floorTxt;
    public Text menu_text;
    

    public GameObject Hamburger_menu;
    public GameObject createPath_menu;
    public GameObject Settings_menu;
    bool Generated = false;


    void Start()
    {

        GlobalControl.FloorLevel = GlobalControl.Floors.G;
        Hamburger_menu.SetActive(false);
        createPath_menu.SetActive(false);
        Settings_menu.SetActive(false);

        spriteNoSign = Map_nosign.GetComponent<SpriteRenderer>();
        spriteSigned = Map_signed.GetComponent<SpriteRenderer>();

        mapUp.onClick.AddListener(TaskOnClickUp);
        mapDown.onClick.AddListener(TaskOnClickDown);
        
        createPath.onClick.AddListener(TaskOnClickCreatePath);
        hamburger.onClick.AddListener(TaskOnClickHamburger);
        hamburgerClose.onClick.AddListener(TaskOnClickHamburgerClose);

  
        generatePath.onClick.AddListener(TaskOnClickGenerate);
        settings.onClick.AddListener(TaskOnClickSettingsMenu);


        back_path.onClick.AddListener(TaskOnClickBackPath);
        back_settings.onClick.AddListener(TaskOnClickBackSettings);

        QRScene_btn.onClick.AddListener(TaskOnClickQR);

        floorTxt.text = "Ground Floor";
     








    }

    private void TaskOnClickQR()
    {
        Debug.Log("QR Click");
        Application.LoadLevel(1);
    }

    void TaskOnClickGenerate()
    {
        
       
           GridManager.GetComponent<BoardCreator>().CreatePath();
        if (Generated == false)
        {
            GridManager.GetComponent<BoardCreator>().GenerateMapVisual();
            Generated = true;

        }

    }


    void TaskOnClickSettingsMenu()
    {
        Settings_menu.SetActive(true);
        menu_text.text = "Settings";
    }

 
    void TaskOnClickQR(string sceneName)
    {
        
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

        if (mapFloor < 5)
        {
            mapFloor++;
            spriteNoSign.sprite = sprites_nosign[mapFloor];
            spriteSigned.sprite = sprites_signed[mapFloor];

            FloorText();
            
        }
    }

    void TaskOnClickCreatePath()
    {
        createPath_menu.SetActive(true);
        menu_text.text = "Path Creator";
    }

    void TaskOnClickDown()
    {
        if (mapFloor > 0)
        {
            mapFloor--;
            spriteNoSign.sprite = sprites_nosign[mapFloor];
            spriteSigned.sprite = sprites_signed[mapFloor];
            FloorText();



        }
    }

    void TaskOnClickHamburger()
    {
        Hamburger_menu.SetActive(true);
        menu_text.text = "Menu";



    }
    void TaskOnClickHamburgerClose()
    {
        Hamburger_menu.SetActive(false);
        createPath_menu.SetActive(false);
        Settings_menu.SetActive(false);
        menu_text.text = "Menu";
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