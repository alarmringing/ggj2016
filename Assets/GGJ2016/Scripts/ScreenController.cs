using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenController : MonoBehaviour {

	public RectTransform WarningTxt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator Damaged(int num) {

		Debug.Log("Damage broadcasted successfully");
		Image flasher = GetComponent<Image>();
		flasher.color=new Color(1f,0f,0f,0.5f);
		yield return new WaitForSeconds(0.3f); 
		if(num == 1)
			GetComponent<Image>().CrossFadeAlpha(0, 3.0f, false);
		else //hit by car, dies
		{
			GetComponent<Image>().CrossFadeAlpha(0.6f, 3.0f, false);
			//WarningTxt.GetComponent<Text>().color = new Color(0f,0f,0f,1f);
			yield return new WaitForSeconds(5f); 
			SceneManager.LoadScene(0);
		}


	}	

}
