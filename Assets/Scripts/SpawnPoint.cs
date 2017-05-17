﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    [SerializeField] private Enemy[] enemies;

	// Use this for initialization
	void Start () {
	    Enemy e = Instantiate(enemies[Random.Range(0, enemies.Length)], transform);
        e.transform.localPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
