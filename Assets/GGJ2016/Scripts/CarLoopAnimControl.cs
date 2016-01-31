using UnityEngine;
using System.Collections;

public class CarLoopAnimControl : MonoBehaviour {
	
	public GameObject otherBarrier;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.tag == "Car")
		{
			Vector3 existingPos = other.gameObject.transform.position;
			Vector3 otherPos = otherBarrier.transform.position;
			if((existingPos.z - otherPos.z) < 0) other.gameObject.transform.position = new Vector3(existingPos.x, existingPos.y, (otherPos.z - 20));
			if((existingPos.z - otherPos.z) > 0) other.gameObject.transform.position = new Vector3(existingPos.x, existingPos.y, (otherPos.z + 20));
		}
	}
}
