using UnityEngine;
using System.Collections;

public class BoardTrain : MonoBehaviour {
	public Transform trainParent;
	public void DoActivateTrigger() {
		Debug.Log("BoardTrain.DoActivateTrigger()!");
		GameObject.FindWithTag("Player").transform.SetParent(trainParent);
		//GameObject.FindWithTag("Player").transform.localPosition=Vector3.zero;
		PlayerPrefs.SetString("Onboard", "true");
		//Application.LoadLevel("CaltrainInside");
	}
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetString("Onboard", "false");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
