﻿using UnityEngine;

public class ActivateTriggerCrosshair : MonoBehaviour 
{
	public enum Mode {
		Trigger   = 0, // Just broadcast the action on to the target
		Replace   = 1, // replace target with source
		Activate  = 2, // Activate the target GameObject
		Enable    = 3, // Enable a component
		Animate   = 4, // Start animation on target
		Deactivate= 5, // Decativate target GameObject
		Message   = 6, // Send a message to the target
		URL 	  = 7, // Open a web page
	}

	/// The action to accomplish
	public Mode action = Mode.Activate;

	/// The game object to affect. If none, the trigger work on this game object
	public bool on = true;
	public Object target;
	public GameObject source;
	public int triggerCount = 1;/// TODO: Triggercount has a bug and doesn't work at 1, works at higher values.
	public bool repeatTrigger = false;
	public string triggerTagRequirement = "Player";
	public bool activateOnCollision = true;
	public bool activateOnShoot = false;
	public string activateOnShootMessage = "";
	public string animationName ="";
	public string messageMethod = "DoActivateTrigger";
	public string messageArgument = "";
	public string url;
	/// Receives this message from the raycasting when the user clicks.
	public void Shoot()
	{
		if(activateOnShoot)
		{
			DoActivateTrigger();
		}
	}
	/// <summary>
	/// DoActivateTrigger receives messages sent to the game object or component.
	/// </summary>
	public void DoActivateTrigger () 
	{
		if(on)
		{
			triggerCount--;

			if (triggerCount > 0 || repeatTrigger) {
				Object currentTarget = target != null ? target : gameObject;
				Behaviour targetBehaviour = currentTarget as Behaviour;
				GameObject targetGameObject = currentTarget as GameObject;
				if (targetBehaviour != null)
					targetGameObject = targetBehaviour.gameObject;
			
				switch (action) {
					case Mode.Trigger:
						
						targetGameObject.BroadcastMessage ("DoActivateTrigger");
						break;
					case Mode.Replace:
						if (source != null) {
							Object.Instantiate (source, targetGameObject.transform.position, targetGameObject.transform.rotation);
							DestroyObject (targetGameObject);
						}
						break;
					case Mode.Activate:
						targetGameObject.active = true;
						break;
					case Mode.Enable:
						if (targetBehaviour != null)
							targetBehaviour.enabled = true;
						break;	
					case Mode.Animate:
					targetGameObject.GetComponent<Animation>().Play(animationName,PlayMode.StopAll);
						break;	
					case Mode.Deactivate:
						targetGameObject.active = false;
						break;
				case Mode.URL:
					Application.OpenURL(url);
					break;
					case Mode.Message:
						if(messageArgument != "")
						{
							if(targetBehaviour!=null)
							{
								targetBehaviour.SendMessage(messageMethod);
							}
							else
							{
								targetGameObject.SendMessage(messageMethod);
							}
						}
						else
						{
							targetGameObject.SendMessage(messageMethod,messageArgument);
						}
						break;
				}
			}
		}
	}

	void OnTriggerEnter (Collider other) 
	{
		if (activateOnCollision)
		{
			if (triggerTagRequirement == "" || other.tag == triggerTagRequirement)
			{
				DoActivateTrigger ();
			}
		}
	}
}