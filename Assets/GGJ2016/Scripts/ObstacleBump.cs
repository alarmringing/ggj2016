using UnityEngine;
using System.Collections;
using UnityStandardAssets;

public class ObstacleBump : MonoBehaviour {

	GameObject UIPanel;
	GameObject player;	
	float bumpTime;
	float currentNerfAmount = 0.9f;
	bool nerfed = false;
	float bumpBufferTime = 3f;

	// Use this for initialization
	void Start () 
	{
		UIPanel = GameObject.FindWithTag("ColorFlashScreen");
		player = GameObject.FindWithTag("Player");
		player.BroadcastMessage("AllowRunning", false);
	}

	// Update is called once per frame
	void Update () 
	{
		if(nerfed && Time.time >( bumpTime + bumpBufferTime))
		{
			player.SendMessage("ReduceWalkingSpeed", 1/currentNerfAmount);
			nerfed = false;
		}

	}
	
	void Bump() {
		nerfed = true;
		bumpTime = Time.time;
		Debug.Log("Player Bump");
		gameObject.GetComponent<AudioSource>().Play();
		player.SendMessage("ReduceWalkingSpeed", currentNerfAmount);
		UIPanel.BroadcastMessage("Damaged", 1);
				
		
	}
}
