using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {


	//spawning
	public GameObject player;
	public Transform doorSpawnTransform;

	//public GameObject doorSpawnObj;
	Vector3 doorSpawnPoint;
	Vector3 bedSpawnPoint;

	void Start()
	{
		Debug.Log("initializing spawn points");
		bedSpawnPoint = player.transform.position;
		doorSpawnPoint = doorSpawnTransform.position;

		Debug.Log("GLobal started, now spawning ");
		Spawn();
	}
		
	void Spawn()
	{
		//GameObject player = GameObject.FindWithTag("Player");



		if(PlayerPrefs.HasKey("SpawnLoc")) //if after first level
		{
			Debug.Log("spawning at specific points");
			int spawnKey = PlayerPrefs.GetInt("SpawnLoc");
			if(spawnKey == 0) //spawn on bed
			{
				player.transform.position = bedSpawnPoint;
			}
			else //spawn next to door
			{
				player.transform.position = doorSpawnPoint;
			}	
		} else
		{
			player.transform.position = bedSpawnPoint;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
