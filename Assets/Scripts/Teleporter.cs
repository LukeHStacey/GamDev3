using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
	public Vector2 direction { 
		get { return _direction; } 
		set { transform.localPosition = value/2;
			_direction = value; }}
	private Vector2 _direction;
	private float timeCreated;
	private bool touchingPlayer, active;

	public Level level { get; set; }
	// Use this for initialization
	void Start () {
		timeCreated = Time.time;
		touchingPlayer = false;
		active = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!active && !touchingPlayer && Time.time > timeCreated + 0.2)
			active = true;
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag.Equals ("Player")) {
			touchingPlayer = true;
			if (active) {
				level.EnterTeleporter (direction);
			}
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag.Equals ("Player")) {
			touchingPlayer = false;
		}
	}
}
