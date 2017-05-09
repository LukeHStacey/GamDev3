using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public int speed = 3;

	static PlayerController _PlayerController;

	void Awake(){
		PlayerController._PlayerController = this;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();
		Vector2 velocity = new Vector2 ();
		if (Input.GetKey(KeyCode.A)){
			velocity.x = -1;
		}else if (Input.GetKey(KeyCode.D)){
			velocity.x = +1;
		}
		if (Input.GetKey(KeyCode.W)){
			velocity.y = 1;
		}else if (Input.GetKey(KeyCode.S)){
			velocity.y = -1;
		}
		rigidBody.velocity = velocity * speed;
	}

	public static PlayerController GetPlayerController(){
		return _PlayerController;
	}
}
