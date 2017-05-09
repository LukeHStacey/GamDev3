using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	static LevelManager _LevelManager;
	[SerializeField]
	private Level[] levelPrefabs;
	private Level currentLevel;
	private Dictionary<Vector2, Level> levels;
	void Awake(){
		LevelManager._LevelManager = this;
	}

	// Use this for initialization
	void Start () {
		levels = new Dictionary<Vector2, Level>();
		GenerateLevel (new Vector2 (0, 0));


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static LevelManager GetLevelManager(){
		return _LevelManager;
	}

	private void GenerateLevel(Vector2 position) {
		currentLevel = Instantiate(levelPrefabs [0]);
		currentLevel.prefab = levelPrefabs [0];
		currentLevel.position = position;
		levels.Add (position, currentLevel);
	}


	public void EnterRoom (Vector2 direction, Vector2 position)
	{
		Vector2 pos = position + direction * currentLevel.size;
		currentLevel.Delete();
		if (!levels.ContainsKey (pos)) {
			GenerateLevel (pos);		
		} else {

			currentLevel = levels [pos];
			currentLevel.reBuild ();
		}

		Transform player =  PlayerController.GetPlayerController ().transform;
		player.position = -(Vector2)player.position;// + direction;
	}
}
