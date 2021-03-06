﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameClock : MonoBehaviour {
	
	public float timeScaleSpeed = 1f;
	public System.DateTime timePassedFormatted;
	public Text clockText;
	int initialHour = 7;
	int initialMin = 56;
	
	public string dateTimeClockString = "XX:XX PM";
	// Use this for initialization
	void Start () {
		//dateTime = new System.DateTime(2016,1,1,8,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if(timeScaleSpeed!=Time.timeScale){
			Time.timeScale=timeScaleSpeed;}
		
		float timePassed = Time.time - PlayerPrefs.GetFloat("GameBeginTime");
		int mins = (int)timePassed / 60 + initialMin;
		int hrs  = (int)(timePassed / 60)/24 + initialHour;
		int secs = (int)(timePassed % 60);
		hrs = hrs - 12*(hrs/12);
		if((mins / (60)) > 0) 
		{
			hrs = hrs + 1;
			mins = mins - 60;		
		}
		PlayerPrefs.SetInt("CurrentHour", hrs);
		PlayerPrefs.SetInt("CurrentMin", mins);

		dateTimeClockString = String.Format("{0:00}:{1:00}:{2:00}", hrs, mins, secs);
		clockText.text = dateTimeClockString + "AM";
	}
}
