using UnityEngine;
using System.Collections;

public class StartBackdropWhenTrainStops : MonoBehaviour {
	
	public GameObject trigger;
	
	public void StartBackdrop()
	{
		trigger.BroadcastMessage("DoActivateTrigger", SendMessageOptions.DontRequireReceiver);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
