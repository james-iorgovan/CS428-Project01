using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;

public class windText : MonoBehaviour
{
    public WeatherInfo2 weatherInfo2;
    public Wind2 main;


    public GameObject timeTextObject;
       string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=b7bf96a0d52a6ae2fb2d5d5f4159fe43&units=imperial";
   
    void Start2()
    {
    // wait a couple seconds to start and then refresh every 30 seconds

       InvokeRepeating("GetDataFromWeb2", 2f, 30f);
   }

   void GetDataFromWeb2()
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
                 weatherInfo2 = JsonUtility.FromJson<WeatherInfo2>(webRequest.downloadHandler.text);
             }
             //Debug.Log(windInfo2.main.temp);
             Debug.Log(weatherInfo2.main.speed);
             timeTextObject.GetComponent<TextMeshPro>().text = weatherInfo2.main.speed.ToString() + "mph" + weatherInfo2.main.deg.ToString() + "°";
        }
     }
}

[System.Serializable]
public class WeatherInfo2
{
    public Coord2 coord;
    public Weather2[] weather; 
    public Temperature2 temperature;
    public Wind2 main;
    public Location2 sys;
    public int visibility;
    public string clouds;
    public int dt;
    public int timezone;    
    public int id;
    public string name;
    public int cod;
    }

[System.Serializable]
public class Coord2
{
    public float lon;
    public float lat;
}

[System.Serializable]
public class Weather2
{
    public int id;
    public string main;
    public string description;
    public string icon;
}

[System.Serializable]
public class Temperature2
{
    public double temp;
    public float feels_like;
    public float temp_min;
    public float temp_max;
    public int pressure;
    public int humidity;
}

[System.Serializable]
public class Wind2
{
    public float speed;
    public int deg;
}

[System.Serializable]
public class Location2
{
    public int type;
    public int id;
    public string country;
    public int sunrise;
    public int sunset;
}