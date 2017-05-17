using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    float difficulty = GetComponentInParent<Level>().difficulty;
	    Enemy e = Instantiate(EnemyManager.Manager.GetEnemyByDifficulty(difficulty) , transform);
        e.transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
