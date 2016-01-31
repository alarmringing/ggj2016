using UnityEngine;
using System.Collections;

public class StartBackdropWhenTrainStops : MonoBehaviour {
	
	public GameObject trigger;
	
	public void StartBackdrop()
	{
		if(PlayerPrefs.GetString("Onboard") == "true")
		{
			trigger.BroadcastMessage("DoActivateTrigger", SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			PlayerPrefs.SetString("Missed", "true");
			Death.Die();
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
