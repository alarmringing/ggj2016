using UnityEngine;
using System.Collections;

public class ToiletController : Seating{

	float startTime;
	float useToiletTime = 30.0f;

	// Use this for initialization
	public void SitOnToilet () {

		startTime = Time.time;

		Debug.Log("SatonToilet. startTime updated.");

		Debug.Log("Sit");
		Sit();		
	}

	// Update is called once per frame
	void Update () {
	
		if(seated == true && Time.time > (startTime + useToiletTime))
		{
			Stand();
			Debug.Log("Stood up");
			GetComponent<AudioSource>().Play();	
		}

	}
}
