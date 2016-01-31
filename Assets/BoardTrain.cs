using UnityEngine;
using System.Collections;

public class BoardTrain : MonoBehaviour {
	public Transform trainParent;
	public void DoActivateTrigger() {
		GameObject.FindWithTag("Player").transform.SetParent(trainParent);
		PlayerPrefs.SetString("Onboard", "true");
		//Application.LoadLevel("CaltrainInside");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
