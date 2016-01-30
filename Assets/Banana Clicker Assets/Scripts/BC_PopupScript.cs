using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BC_PopupScript : MonoBehaviour {

    public float clickRate;
    public float popupScrollSpeed = 10f;
    public UnityEngine.UI.Text popupText;
    public Transform _canvas;


    //this is the first function to run when the app gets started.
    void Awake()
    {

        //find object called click and get a reference to the script it contains called "BC_Click"
        clickRate = GameObject.Find("Click").GetComponent<BC_Click>().bananasPerClick;
        //get reference to the text componet on this object
        popupText = GetComponent<Text>();
        
        //set the text component to + with the clickrate.
        popupText.text = "+" + clickRate;

        //set the parent of the object to the canvas, so that the position of the popus is right.
        transform.SetParent(GameObject.Find("Canvas").transform, false) ;
        //destroy the popup after 1 second.
        Destroy(gameObject, 1f);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //every fram smoothly move up
        transform.Translate(Vector2.up * popupScrollSpeed * Time.deltaTime);



    } 
}
