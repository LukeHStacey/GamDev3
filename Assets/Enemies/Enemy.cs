using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    PlayerController player = PlayerController.GetPlayerController();
	    Vector2 direction = (player.transform.position - transform.position);
	    if (direction.magnitude > 0.1) {
	        transform.position =  Vector2.Lerp(transform.position, player.transform.position, speed * Time.deltaTime / direction.magnitude);
	    }
	}
}
