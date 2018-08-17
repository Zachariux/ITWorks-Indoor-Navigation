using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FindLocation : MonoBehaviour {


    public Text LocationInfo;

    public float latitude = 1;
    public float longitude = 1;
    public float altitude = 1;
    public float horizontalAccuracy;
    public string timestamp;

    IEnumerator Start()
    {
        // First, check if user has location service enabled
       // LocationInfo.text = "Location Test Start...";
        if (!Input.location.isEnabledByUser)
        {
            yield break;
    
        }
        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            LocationInfo.text = "maxWait=="+ maxWait;
            yield return new WaitForSeconds(1);
            maxWait--;
            
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            LocationInfo.text = "Timed out";
            print("Timed out");
            longitude = 2;
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            LocationInfo.text = "Unable to determine device location";
            print("Unable to determine device location");
            longitude = 3;
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            altitude = Input.location.lastData.altitude;
            horizontalAccuracy = Input.location.lastData.horizontalAccuracy;
            timestamp = Input.location.lastData.timestamp.ToString();

            LocationInfo.text = "Latitude= " + latitude + "\nLongitude= " + longitude + "\nAltitude= " + altitude + "\nHorizontalAccuracy= " + horizontalAccuracy + "\nTimestamp= " + timestamp;
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }
    private void Update()
    {
        Start();
    }
}