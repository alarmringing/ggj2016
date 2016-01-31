using UnityEngine;
using System.Collections;

public class TrainAnimationControl : MonoBehaviour {
	
	public GameObject playFirstFrame;
	
	// Use this for initialization
	void Start () {
		
		//playFirstFrame.GetComponent<Animation>().Play("trainTest",PlayMode.StopSameLayer);
	}
	int frame = 0;
	// Update is called once per frame
	void Update () {
		frame ++;
		if(frame>2)
		{
			foreach (AnimationState animState in playFirstFrame.GetComponent<Animation>())
			{
				//animState.speed=0f;
			}	
		}
	}
	
	public void DoActivateTrigger()
	{
		Debug.Log("DO ACTIVATE TRIGGER");
		foreach (AnimationState animState in playFirstFrame.GetComponent<Animation>())
		{
			//animState.speed=0f;
		}
		//playFirstFrame.GetComponent<Animation>().Play();
		//playFirstFrame.GetComponent<Animation>().enabled=true;
	}
}
