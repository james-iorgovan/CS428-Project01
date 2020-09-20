using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;

public class WeatherAPIScript : MonoBehaviour
{
    public WeatherInfo weatherInfo;
    public Temperature main;


    public GameObject timeTextObject;
       string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=&units=imperial";
   
    void Start()
    {
    // wait a couple seconds to start and then refresh every 30 seconds

       InvokeRepeating("GetDataFromWeb", 2f, 30f);
   }

   void GetDataFromWeb()
   {

       StartCoroutine(GetRequest(url));
   }

     private IEnumerator GetRequest(string uri)
     {
         using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
         {
             // Request and wait for the desired page.
             yield return webRequest.SendWebRequest();


             if (webRequest.isNetworkError)
             {
                 Debug.Log(": Error: " + webRequest.error);
             }
             else
             {
                 // print out the weather data to make sure it makes sense
                 weatherInfo = JsonUtility.FromJson<WeatherInfo>(webRequest.downloadHandler.text);
             }
            //  Debug.Log(weatherInfo.main.temp);
             timeTextObject.GetComponent<TextMeshPro>().text = "Temperature: " + weatherInfo.main.temp.ToString() + " F\n" + "Humidity: " + weatherInfo.main.humidity.ToString() + "%";
             print("hello8");
             //timeTextObject.GetComponent<TextMeshPro>().text = weatherInfo.main.humidity.ToString() + "%";
        }
     }
}

[System.Serializable]
public class WeatherInfo
{
    public Coord coord;
    public Weather[] weather; 
    public Temperature main;
    public Wind wind;
    public Location location;
    public int visibility;
    public string clouds;
    public int dt;
    public int timezone;    
    public int id;
    public string name;
    public int cod;
    }

[System.Serializable]
public class Coord
{
    public float lon;
    public float lat;
}

[System.Serializable]
public class Weather
{
    public int id;
    public string main;
    public string description;
    public string icon;
}

[System.Serializable]
public class Temperature
{
    public double temp;
    public float feels_like;
    public float temp_min;
    public float temp_max;
    public int pressure;
    public int humidity;
}

[System.Serializable]
public class Wind
{
    public float speed;
    public int deg;
}

[System.Serializable]
public class Location
{
    public int type;
    public int id;
    public string country;
    public int sunrise;
    public int sunset;
}
