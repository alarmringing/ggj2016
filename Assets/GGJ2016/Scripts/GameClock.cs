using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameClock : MonoBehaviour {
	
	public System.DateTime dateTime;
	public Text clockText;
	
	public string dateTimeClockString = "XX:XX PM";
	// Use this for initialization
	void Start () {
		dateTime = System.DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
		dateTime = System.DateTime.Now;
		dateTimeClockString = dateTime.ToLongTimeString() + " " + dateTime.ToString("tt", System.Globalization.CultureInfo.InvariantCulture);
		clockText.text = dateTimeClockString;
	}
}
