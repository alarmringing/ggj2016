using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {


	//spawning
	public GameObject doorSpawnObj;
	Vector3 doorSpawnPoint;
	Vector3 bedSpawnPoint;

	void Start()
	{
		Spawn();
	}
		
	void Spawn()
	{
		GameObject player = GameObject.Find("Player");

		if(PlayerPrefs.HasKey("SpawnLoc")) //if after first level
		{
			int spawnKey = PlayerPrefs.GetInt("SpawnLoc");
			if(spawnKey == 0) //spawn on bed
			{
				player.transform.position = bedSpawnPoint;
			}
			else //spawn next to door
			{
				player.transform.position = doorSpawnPoint;
			}	
		}
		else //if first level, initialize spawn points
		{
			Debug.Log("initializing spawn points");
			bedSpawnPoint = player.transform.position;
			doorSpawnPoint = doorSpawnObj.transform.position;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
