using UnityEngine;
using System.Collections;
using System;

public class GameClock : MonoBehaviour {
	
	public System.DateTime dateTime;
	
	public string dateTimeClockString = "XX:XX PM";
	// Use this for initialization
	void Start () {
		dateTime = System.DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
		dateTimeClockString = dateTime.ToShortTimeString();
	}
}
