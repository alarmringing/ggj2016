using UnityEngine;
using System.Linq;
using System.Collections;

public class CarAnimControl : MonoBehaviour {

	float velocity;


	// Use this for initialization
	void Start () {
		velocity = (float)(Random.Range(1,6)*0.1); //assign random velocity for each cars
		Debug.Log("Velocity for this car " + gameObject.name +" is " + velocity);

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
}
