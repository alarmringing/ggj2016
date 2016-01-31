using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameClock : MonoBehaviour {
	
	public System.DateTime timePassedFormatted;
	public Text clockText;
	
	public string dateTimeClockString = "XX:XX PM";
	// Use this for initialization
	void Start () {
		//dateTime = new System.DateTime(2016,1,1,8,0,0);
	}
	
	// Update is called once per frame
	void Update () {

		float timePassed = Time.time - PlayerPrefs.GetFloat("GameBeginTime");
		//Debug.Log("Now time passed is" + timePassed);
		//timePassedFormatted = new System.DateTime((long)timePassed);
		///string dateTimeClockString = (int)(timePassed / 60) + ":" + (int)(timePassed % 60);
		int mins = (int)timePassed % 60;
		int hrs  = (int)(timePassed / 60) + 7;
		string ampm = "";
		hrs = hrs - 12*(hrs/12);
		if((hrs / 12) %2 == 1) 
		{
			ampm = "PM";
		}
		else if ((hrs / 12) %2 == 0)
		{
			ampm = "AM";
		}
		//string dateTimeClockString = (hrs + 6) + ":" + mins;

		dateTimeClockString = String.Format("{0:00}:{1:00}", hrs, mins);
		//dateTimeClockString = timePassedFormatted.ToShortTimeString()/* + " " + dateTime.ToString("tt", System.Globalization.CultureInfo.InvariantCulture)*/;
		clockText.text = dateTimeClockString + ampm;
	}
}
