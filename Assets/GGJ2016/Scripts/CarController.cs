using UnityEngine;
using System.Linq;
using System.Collections;

public class CarController : MonoBehaviour {

	GameObject UIPanel;
	GameObject player;
	Camera playerCamera;
	bool hittingPlayer = false;
	float hitBeginTime;
	float hitBufferTime = 1.7f;

	float velocity;


	// Use this for initialization
	void Start () {

		UIPanel = GameObject.FindWithTag("ColorFlashScreen");
		player = GameObject.FindWithTag("Player");
		velocity = (float)(Random.Range(3,6)*0.1); //assign random velocity for each cars

	}
	
	// Update is called once per frame
	void Update () {

		//iTween.MoveAdd(gameObject, iTween.Hash("x", transform.position.x - 2f, "time", velocity, "easetype", iTween.EaseType.linear));

		if(transform.rotation.y > 0) 
		{
			transform.position = transform.position + new Vector3(0,0,velocity);
		}
		else transform.position = transform.position - new Vector3(0,0,velocity);

		if(hittingPlayer)
		{
			if(Time.time > hitBeginTime + hitBufferTime)
			{
				player.GetComponent<Rigidbody>().isKinematic = true;
				player.GetComponent<CharacterController>().enabled = true;
			}
		}


	}

	public void HitPlayer() 
	{
		Debug.Log("Player dead");
		hittingPlayer = true;
		hitBeginTime = Time.time;

		GetComponent<AudioSource>().Play();
	
		player.SendMessage("ReduceWalkingSpeed", 0.6); //cripple walking 
		UIPanel.BroadcastMessage("Damaged", 1); //flash screen	

		float thrust = 100f;
		//player.GetComponent<Rigidbody>().ki;
		player.GetComponent<CharacterController>().enabled = false;
		player.GetComponent<Rigidbody>().isKinematic = false;
		player.GetComponent<Rigidbody>().AddForce(new Vector3(0.4f,2f,0) * thrust);
		player.GetComponent<Rigidbody>().useGravity = true;

		//yield return new WaitForSeconds(0.2f); 


		//player.GetComponent<CharacterController>().enabled = true;

	}
		
	/*
	void OnTriggerEnter(Collider col) {
		//if(col.gameObject.tag == "Car")
		Debug.Log("this car is hitting something");
			
		if(col.gameObject.tag == "Player") 
		{
			GetComponent<AudioSource>().Play();
			Debug.Log("HONK");
		}
	}*/
	
	
	
}
