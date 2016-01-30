using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour {

	public int levelNum;
	bool activated = false;

	public void Exit() {

		activated = true; 

		Debug.Log("Exiting House");
		GetComponent<AudioSource>().Play();

		//Level change if audio finishes
		//yield WaitForSeconds(1);


	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(activated == true && !GetComponent<AudioSource>().isPlaying) //wait until audio finishes
		{
			PlayerPrefs.SetInt("SpawnLoc", 1);
			SceneManager.LoadScene(levelNum);

		}
	}
}
