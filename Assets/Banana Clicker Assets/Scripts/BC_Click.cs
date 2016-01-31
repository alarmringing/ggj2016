using UnityEngine;
using System.Collections;
using Parse;


public class BC_Click : MonoBehaviour {

	//this allows you to reference the bananas per click and the main banana display by dragging them into the inspector.
	public UnityEngine.UI.Text bpc;
	public UnityEngine.UI.Text bananaDisplay;
    //public UnityEngine.UI.Text guiText;
    public GameObject popupText;
    public GameObject popupLoc;





	//this is your main total of bananas
	public float bananas = 0;

	//this is the starting rate of bananas per click.
	public float bananasPerClick = 1;






	//this updates your total bananas constantly.
	void Update()
	{


		bananaDisplay.text = BC_currencyConverter.Instance.GetCurrencyIntoString (bananas, false, false);
		bpc.text = BC_currencyConverter.Instance.GetCurrencyIntoString (bananasPerClick, false, true);






	}

    public void Attack()
    {
        this.bananas /= 2.0f;
    }

	//pretty simply when you click this happens and adds the bananas perclick to your total.
	public void Clicked()
	{
		bananas += bananasPerClick;

        //this instantiates a text object that says the total clicks it just added, it contains a script that makes it smoothly float up and disappear.
        GameObject newObject = (GameObject)Instantiate(popupText, Vector3.zero, Quaternion.identity);
        newObject.transform.SetParent(popupLoc.transform, false);
        //newObject.GetComponent<RectTransform>().anchoredPosition = popupLoc.GetComponent<RectTransform>().anchoredPosition;

        //this is the old way i use to show the message.
        //StartCoroutine(ShowMessage("+" + bananasPerClick, 1));


	}

    //this has been replaced in the new version with a better method of showing the popup, but i have left it here so you learn the same way i did.
	IEnumerator ShowMessage (string message, float delay) {
		GetComponent<GUIText>().text = message;
		GetComponent<GUIText>().enabled = true;
		yield return new WaitForSeconds(delay);
		GetComponent<GUIText>().enabled = false;
	}



}
