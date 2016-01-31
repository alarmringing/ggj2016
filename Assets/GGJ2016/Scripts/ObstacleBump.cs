using UnityEngine;
using System.Collections;
using UnityStandardAssets;

public class ObstacleBump : MonoBehaviour {

	GameObject UIPanel;
	GameObject player;

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
		
	}
	
	void Bump() {
		Debug.Log("Player Bump");
		gameObject.GetComponent<AudioSource>().Play();
		player.SendMessage("ReduceWalkingSpeed", 0.8);
		UIPanel.BroadcastMessage("Damaged", 0);
	}
}
