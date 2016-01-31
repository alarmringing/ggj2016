using UnityEngine;
using System.Collections;

public class IpadController : MonoBehaviour {
	
	
	GameObject player;
	GameObject playerController;
	Camera playerCamera;
	GameObject playerUI;
	
	public Camera iPadCamera;
	float bufferTime = 0.5f;
	float startingTime;
	
	bool LookingAt = false;
	
	public void LookAtIpad() {
		Debug.Log("Looking At Ipad!");
		
		startingTime = Time.time;
		LookingAt = true;
		iPadCamera.enabled = true;
		playerUI.SetActive(false);
		playerCamera.enabled = false;
		
		
	}
	
	// Use this for initialization
	void Start () {
				
		player = GameObject.FindWithTag("Player");
		playerController = GameObject.FindWithTag("GameController");
		playerCamera = (GameObject.FindWithTag("MainCamera")).GetComponent<Camera>();
		playerUI = GameObject.FindWithTag("CrosshairGUI");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Time.time > (startingTime + bufferTime) && LookingAt && Input.GetMouseButtonDown(0)) //click to exit
		{
			Debug.Log("leaving ipad focus mode");
			iPadCamera.enabled = false;
			playerUI.SetActive(true);
			playerCamera.enabled = true;
			LookingAt = false;
		}
	}
}
