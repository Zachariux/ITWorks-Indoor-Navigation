using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ButtonClick2 : MonoBehaviour {

    public Button QR_scene_btn;
	// Use this for initialization
	void Start () {
        QR_scene_btn.onClick.AddListener(TaskOnClickQR);
    }

    private void TaskOnClickQR()
    {
        Application.LoadLevel("QRcode");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
