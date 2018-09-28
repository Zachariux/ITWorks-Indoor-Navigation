using BarcodeScanner;
using BarcodeScanner.Scanner;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Wizcorp.Utils.Logger;


public class QRScanner : MonoBehaviour {

    private IScanner BarcodeScanner;
    public Text TextHeader;
    public RawImage Image;
    public Button StartButton;
    public Button StopButton;
    public Button BackButton;

    //Disable Screen Rotation
    private void Awake()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }

    // Use this for initialization
    void Start() {

        //Adding Button Method Listener
        StartButton.onClick.AddListener(start_Clicked);
        StopButton.onClick.AddListener(stop_Clicked);
        BackButton.onClick.AddListener(back_Clicked);


        //Create The Scanner
        BarcodeScanner = new Scanner();
        //Set the Camera/Webcam to play/record
        BarcodeScanner.Camera.Play();

        //Display The Camera in a Raw Image Control
        BarcodeScanner.OnReady += (sender, arg) =>
        {
            //Set Orientation
            Image.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles(); //Degrees around X and Y axis
            Image.transform.localScale = BarcodeScanner.Camera.GetScale(); //Scale relative to parent
            //Set Texture
            Image.texture = BarcodeScanner.Camera.Texture; //Getting the live webcame texture as the image texture

            //Keep Image Aspect Ratio
            var rect = Image.GetComponent<RectTransform>();
            var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);
        };

        //Track the Status of the Scanner
        BarcodeScanner.StatusChanged += (sender, arg) =>
        {
            TextHeader.text = "Status: " + BarcodeScanner.Status;
        };
	}
	
	// Update is called once per frame
	void Update () {
        if (BarcodeScanner == null)
        {
            return;
        }
        BarcodeScanner.Update();
		
	}

    #region UI Buttons
    //The Start Button is Clicked
    public void start_Clicked()
    {
        if (BarcodeScanner == null)
        {
            Log.Warning("No Valid Camera - Click Start");
            return;
        }

        //Start Scanning
        BarcodeScanner.Scan((barcodeType, barcodeValue) =>
        {
            BarcodeScanner.Stop();
            TextHeader.text = "Found: " + barcodeType + " | " + barcodeValue;
        });
    }

    public void stop_Clicked()
    {
        if (BarcodeScanner == null)
        {
            Log.Warning("No Valid Camera - Click Stop");
            return;
        }

        //Stop Scanning
        BarcodeScanner.Stop();
    }

    public void back_Clicked()
    {
        //Try to stop the camera before loading another scene
        StartCoroutine(StopCamera(() =>
        {
            SceneManager.LoadScene("Nav1");
        }));
    }

    /// <summary>
	/// This coroutine is used because of a bug with unity (http://forum.unity3d.com/threads/closing-scene-with-active-webcamtexture-crashes-on-android-solved.363566/)
	/// Trying to stop the camera in OnDestroy provoke random crash on Android
	/// </summary>
	/// <param name="callback"></param>
	/// <returns></returns>
	public IEnumerator StopCamera(Action callback)
    {
        // Stop Scanning
        Image = null;
        BarcodeScanner.Destroy();
        BarcodeScanner = null;

        // Wait a bit
        yield return new WaitForSeconds(0.1f);

        callback.Invoke();
    }
    #endregion
}
