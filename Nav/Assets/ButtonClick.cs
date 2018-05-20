using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ButtonClick : MonoBehaviour
{

    public Button mapUp;
    public Button mapDown;
    public Button hamburger;
    public Button hamburgerClose;
    public Button hamburgerClose2;
    public Button createPath;
    public Button generatePath;

    public GameObject GridManager;

    public GameObject Map;
    public int mapFloor = 1;
    public SpriteRenderer spriteR;
    public Sprite[] sprites;
    public Text floorTxt;

    public GameObject Hamburger_menu;
    public GameObject createPath_menu;

    void Start()
    {


        Hamburger_menu.SetActive(false);
        createPath_menu.SetActive(false);
        spriteR = Map.GetComponent<SpriteRenderer>();
        mapUp.onClick.AddListener(TaskOnClickUp);
        mapDown.onClick.AddListener(TaskOnClickDown);
        
        createPath.onClick.AddListener(TaskOnClickCreatePath);
        hamburger.onClick.AddListener(TaskOnClickHamburger);
        hamburgerClose.onClick.AddListener(TaskOnClickHamburgerClose);
        hamburgerClose2.onClick.AddListener(TaskOnClickHamburgerClose2);
        generatePath.onClick.AddListener(TaskOnClickGenerate);

        floorTxt.text = "Ground Floor";
     

    }

    void TaskOnClickGenerate()
    {
        GridManager.GetComponent<BoardCreator>().GenerateMapData();

        GridManager.GetComponent<BoardCreator>().GeneratePathfindingGraph();
        GridManager.GetComponent<BoardCreator>().Setup();

    }

    void TaskOnClickUp()
    {
        if (mapFloor < 5)
        {
            mapFloor++;
            spriteR.sprite = sprites[mapFloor];
            FloorText();
        }
    }

    void TaskOnClickCreatePath()
    {
        createPath_menu.SetActive(true);
    }

    void TaskOnClickDown()
    {
        if (mapFloor > 0)
        {
            mapFloor--;
            spriteR.sprite = sprites[mapFloor];
            FloorText();
        }
    }

    void TaskOnClickHamburger()
    {
        Hamburger_menu.SetActive(true);
    }
    void TaskOnClickHamburgerClose()
    {
        Hamburger_menu.SetActive(false);
    }

    void TaskOnClickHamburgerClose2()
    {
        Hamburger_menu.SetActive(false);
        createPath_menu.SetActive(false);
    }


    void FloorText()
    {
        switch (mapFloor)
        {
            case 5:
                floorTxt.text = "4F";
                break;
            case 4:
                floorTxt.text = "3F";
                break;
            case 3:
                floorTxt.text = "2F";
                break;
            case 2:
                floorTxt.text = "1F";
                break;
            case 1:
                floorTxt.text = "Ground Floor";
                break;
            default:
                floorTxt.text = "Basement";
                break;
        }
    }
}