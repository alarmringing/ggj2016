﻿using UnityEngine;
using System.Collections;

public class Seating : MonoBehaviour {
	
	public bool seated = false;
	public GameObject player;
	public Vector3 standingPosition;
	public Transform seatedPositionTransform;
	public ActivateTriggerCrosshair sitTrigger;
	public ActivateTriggerCrosshair standTrigger;
	
	public void Sit()
	{
		Debug.Log("Sitting");
		seated = true;
		standingPosition = player.transform.position;
		CharacterController cc = player.GetComponent<CharacterController>();
		cc.height = 0.6f;
		player.transform.position = seatedPositionTransform.position;
		sitTrigger.on = false;
		standTrigger.on = true;
		cc.enabled=false;
		
		
	}
	
	public virtual void Stand()
	{
		CharacterController cc = player.GetComponent<CharacterController>();
		cc.height = 1.8f;
		cc.enabled=true;
		seated = false;
		player.transform.position = standingPosition;
		sitTrigger.on = true;
		standTrigger.on = false;
	}
	
	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(seated)
		{
			player.transform.position = seatedPositionTransform.position;
			if(Input.GetAxis("Vertical") < 0 )
			{
				Stand();
			}
		}
	}
}
