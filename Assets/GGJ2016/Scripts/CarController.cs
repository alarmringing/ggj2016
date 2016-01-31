using UnityEngine;
using System.Linq;
using System.Collections;

public class CarController : MonoBehaviour {

	float velocity;


	// Use this for initialization
	void Start () {
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
	}

	void OnCollisionEnter(Collision col) {
		//if(col.gameObject.tag == "Car")
		Debug.Log("this car is hitting something");
			
		if(col.gameObject.tag == "Player") 
		{
			Debug.Log("I died");
		}
	}
	
	
	
}
