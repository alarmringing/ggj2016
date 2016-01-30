using UnityEngine;
using System.Collections;

public class BC_MenuManager : MonoBehaviour {

    Animator anim;



    void Awake()
    {
        //this makes a ref to the animator on startup
        anim = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //function runs on click and simply sets a bool in the animator to say ShowMenu
    public void ShowMenu()
    {
        anim.SetTrigger("ShowMenu");
    }
}
