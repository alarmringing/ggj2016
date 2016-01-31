using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPhone : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		if(!GameObject.Find("Phone View"))
		{
			SceneManager.LoadScene("PhoneScene", LoadSceneMode.Additive);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
