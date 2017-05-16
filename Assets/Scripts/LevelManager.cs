using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour {

    static LevelManager _LevelManager;
    [SerializeField]
    private Level[] levelPrefabs;

    [SerializeField]
    private Level startingRoom;
    private Level currentLevel;
    private Dictionary<Vector2, Level> levels;
    void Awake() {
        LevelManager._LevelManager = this;
    }

    // Use this for initialization
    void Start() {
        levels = new Dictionary<Vector2, Level>();
        GenerateLevel(startingRoom, new Vector2(0, 0));


    }

    // Update is called once per frame
    void Update() {

    }

    public static LevelManager GetLevelManager() {
        return _LevelManager;
    }

    private void GenerateLevel(Level prefab, Vector2 position) {
        currentLevel = Instantiate(prefab);
        currentLevel.prefab = prefab;
        currentLevel.position = position;
        levels.Add(position, currentLevel);
    }

    private void GetNextLevel(Vector2 position) {
        //Variance of room allowed
        float roomRange = levelPrefabs.Length * 0.2f;
        //Number of room until gaurenteed Boss
        int gaurnteedBoss = 10;
        float lowerLim = (position.magnitude * (levelPrefabs.Length)) / gaurnteedBoss;

        int room = (int) Mathf.Clamp(Mathf.Floor(Random.Range(lowerLim, lowerLim + roomRange)), 0, levelPrefabs.Length);

        GenerateLevel(levelPrefabs[room], position);
    }

    public void EnterRoom(Vector2 direction, Vector2 roomPosition) {
        currentLevel.Delete();
        if(!levels.ContainsKey(roomPosition)) {
            GetNextLevel(roomPosition);
        }
        else {

            currentLevel = levels[roomPosition];
            currentLevel.reBuild();
        }

        Transform player = PlayerController.GetPlayerController().transform;
        player.position = -(Vector2) player.position;// + direction;
    }

    void OnGUI() {
        GUI.Label(new Rect(0, 0, 100, 100), currentLevel.ToString());
    }

}
