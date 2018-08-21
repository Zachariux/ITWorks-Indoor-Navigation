using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public GameObject showStairs;
    public Toggle showStairs_toggle;
    public GameObject groundFloor_signs;


	void Start () {
        groundFloor_signs.SetActive(false);
        showStairs_toggle = showStairs.GetComponent<Toggle>();


        showStairs_toggle.onValueChanged.AddListener(delegate {
            StairToggleValueChanged(showStairs_toggle);
        });

    }


    void StairToggleValueChanged(Toggle change)
    {
       if(showStairs_toggle.isOn == true)
        {
            groundFloor_signs.SetActive(true);
        }

        if (showStairs_toggle.isOn == false)
        {
            groundFloor_signs.SetActive(false);
        }

    }
    // Update is called once per frame

}
