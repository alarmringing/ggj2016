using UnityEngine;
using System.Collections;

public class LoadPhone : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		if(!GameObject.Find("Phone View"))
		{
			Application.LoadLevelAdditive("PhoneScene");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
