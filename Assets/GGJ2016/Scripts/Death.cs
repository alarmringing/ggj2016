using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {
	
	public static void Die()
	{
		GameObject.Destroy(GameObject.Find("PhoneScene"));
		SceneManager.LoadScene("Home");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
