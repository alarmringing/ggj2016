using UnityEngine;
using System.Collections;

public class ToiletController : Seating{

	public AudioClip peeing;
	public AudioClip flushing;
	AudioSource soundToPlay;

	float startTime;
	float useToiletTime = 30.0f;

	void Awake()
	{
		soundToPlay = GetComponent<AudioSource>();
	}

	// Use this for initialization
	public void SitOnToilet () {

		startTime = Time.time;

		Debug.Log("SatonToilet. startTime updated.");
		Debug.Log("Sit");

		soundToPlay.clip = peeing;
		soundToPlay.Play();
		Sit();		
	}

	// Update is called once per frame
	void Update () {
	
		if(seated == true && Time.time > (startTime + useToiletTime))
		{
			Stand();
			Debug.Log("Stood up");
			soundToPlay.clip = peeing;
			soundToPlay.Stop();
			soundToPlay.clip = flushing;
			soundToPlay.Play();
		}

	}
}
