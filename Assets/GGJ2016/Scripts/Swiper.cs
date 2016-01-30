using UnityEngine;
using System.Collections;

public class Swiper : MonoBehaviour {
	
	public void Swipe () {
		Debug.Log("Swiped");
		GetComponent<AudioSource>().Play();	
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
