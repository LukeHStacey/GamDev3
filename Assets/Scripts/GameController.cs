using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	static GameController _GameController;

	void Awake(){
		GameController._GameController = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static GameController GetGameController(){
		return _GameController;
	}
}
