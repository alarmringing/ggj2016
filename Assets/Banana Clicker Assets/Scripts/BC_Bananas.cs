using UnityEngine;
using System.Collections;

public class BC_Bananas : MonoBehaviour {

	public float delay = 0.1f;

	public GameObject bananap;



	// Use this for initialization
	void Start () {

            //this means repeat Spawn every Delay, with the first delay being set here too.
			InvokeRepeating ("Spawn", delay, delay);



	
	}
	
	// Update is called once per frame
	public void Spawn () {
        //this instantiate the bananas. old code but i have left it here for you to learn from
		Instantiate (bananap, new Vector3 (Random.Range (25, 1020), 500 ,524 ), Quaternion.identity);
	
	}
}
