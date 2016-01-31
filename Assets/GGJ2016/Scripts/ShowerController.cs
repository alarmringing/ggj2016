using UnityEngine;
using System.Collections;

public class ShowerController : Seating {

	float startTime;
	float useShowerTime = 20.0f;

	// Use this for initialization
	public void StartShower () {

		startTime = Time.time;

		Debug.Log("SatonToilet. startTime updated.");
		GetComponent<AudioSource>().Play();	
		Debug.Log("Sit");
		Sit();		
	}

	// Update is called once per frame
	void Update () {

		if(seated == true && Time.time > (startTime + useShowerTime))
		{
			GetComponent<AudioSource>().Stop();
			Stand();
			Debug.Log("Left Shower");
		}

	}
}
