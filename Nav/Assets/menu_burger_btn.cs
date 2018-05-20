using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menu_burger_btn : MonoBehaviour {

    public Button menu;
    // Use this for initialization
    void Start () {
        menu.onClick.AddListener(TaskOnClickMenu);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TaskOnClickMenu()
    {

        Destroy(this.gameObject);
    }

}
