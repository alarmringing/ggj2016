using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
	
	public static void Die()
	{
		GameObject.Destroy(GameObject.Find("PhoneScene"));
		Application.LoadLevel("Home");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
