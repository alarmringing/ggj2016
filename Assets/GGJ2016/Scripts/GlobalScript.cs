using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {


	//spawning
	GameObject player;
	GameObject playerController;
	public Transform doorSpawnTransform;

	//keeping track of it it has been visited
	static bool hasBeenVisited = false;

	//public GameObject doorSpawnObj;
	Vector3 doorSpawnPoint;
	Vector3 bedSpawnPoint;

	void Start()
	{
		player = GameObject.FindWithTag("Player");
		playerController = GameObject.FindWithTag("GameController");
		
		Debug.Log("initializing spawn points");
		bedSpawnPoint = playerController.transform.position;
		doorSpawnPoint = doorSpawnTransform.position;

		Debug.Log("GLobal started, now spawning ");
		Spawn();
	}
		
	void Spawn()
	{
	
		if(PlayerPrefs.HasKey("SpawnLoc")) //if after first level
		{
			Debug.Log("spawning at specific points");
			//int spawnKey = PlayerPrefs.GetInt("SpawnLoc");
			if(!hasBeenVisited) //spawn on bed
			{
				playerController.transform.position = bedSpawnPoint;
				hasBeenVisited = true;
			}
			else //spawn next to door
			{
				playerController.transform.position = doorSpawnPoint;
			}	
		} else
		{
			playerController.transform.position = bedSpawnPoint;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
