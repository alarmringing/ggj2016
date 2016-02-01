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
	
	void Start () {
		Object.DontDestroyOnLoad(GameObject.Find("PhoneScene"));
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
