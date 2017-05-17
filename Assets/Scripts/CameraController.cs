using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
		Vector3 playerPos = PlayerController.GetPlayerController().transform.position;
		playerPos.z = -10;
		transform.position = playerPos;

	}
}
