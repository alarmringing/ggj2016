using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenController : MonoBehaviour {


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
		if(num == 0)
			GetComponent<Image>().CrossFadeAlpha(0, 3.0f, false);
		else
			GetComponent<Image>().CrossFadeAlpha(0.6f, 3.0f, false);
	}	

}
