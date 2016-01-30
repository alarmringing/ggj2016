using UnityEngine;
using System.Collections;

public class ShowerController : MonoBehaviour {

	public void StartShower() {
		Debug.Log("Showering");
		GetComponent<AudioSource>().Play();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
