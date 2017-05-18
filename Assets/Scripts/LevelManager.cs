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
    private int gaurnteedBoss = 10;
    void Awake() {
        LevelManager._LevelManager = this;
    }

    // Use this for initialization
    void Start() {
        levels = new Dictionary<Vector2, Level>();
        currentLevel = Level.GenerateLevel(startingRoom, Vector2.zero, 0);

        levels.Add(Vector2.zero, currentLevel);

    }

    // Update is called once per frame
    void Update() {

    }

    public static LevelManager GetLevelManager() {
        return _LevelManager;
    }

    private void GetNextLevel(Vector2 position) {
        int room = GetObjectByDifficulty(position.magnitude, levelPrefabs.Length);
        currentLevel = Level.GenerateLevel(levelPrefabs[room], position, position.magnitude/gaurnteedBoss);
    }

    private int GetObjectByDifficulty(float difficulty, int length) {
        //Variance of room allowed
        float roomRange = length * 0.2f;
        //Number of room until gaurenteed Boss
        float lowerLim = (difficulty * (length)) / gaurnteedBoss;
        return (int) Mathf.Clamp(Mathf.Floor(Random.Range(lowerLim, lowerLim + roomRange)), 0, levelPrefabs.Length-1);
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
        PlayerController.GetPlayerController().OnEnterRoom();
        player.position = currentLevel.getTeleportPosition(-direction) + direction * 0.15f; 


        float red = 1 - (gaurnteedBoss - roomPosition.magnitude) / gaurnteedBoss;
        float green = 1 - red;
        Color bgColor = new Color(red, green, 0);
        Background.GetBackground().GetComponent<SpriteRenderer>().color = bgColor;
    }

    void OnGUI() {
        GUI.Label(new Rect(0, 0, 100, 100), currentLevel.ToString());
    }

}
