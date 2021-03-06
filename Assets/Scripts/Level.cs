﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour {
    [SerializeField]
    private Teleporter teleporterPrefab;
    [SerializeField]
    private GameObject[] corners;
    [SerializeField]
    private GameObject[] walls;
    [SerializeField]
    private GameObject[] floor;
    [SerializeField]
    private float x, y;

    [SerializeField] private GameObject _floor;

    public Vector2 dimensions {
        get { return new Vector2(x, y); }
        set {
            x = value.x;
            y = value.y;
        }
    }
    public float difficulty { get; private set; }
    public Level prefab { get; set; }
    public Vector2 position { get; set; }

    private int _enimies;
    public int Enemies {
        get { return _enimies; }
        set {
            if (value == 0 && _enimies != 0) {
                Debug.Log("Spawning an Item!");
                Instantiate(ItemManager.Manager.GetItem(), transform);
            }
            _enimies = Math.Max(value, 0);
        }
    }

    private Vector2[] directions = new Vector2[4] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    // Use this for initialization
    void Start() {
    }

    public static Level GenerateLevel(Level prefab, Vector2 position, float difficulty) {
        Level currentLevel = Instantiate(prefab);
        currentLevel.prefab = prefab;
        currentLevel.position = position;
        currentLevel.difficulty = difficulty;
        currentLevel.Generate();

        SpawnPoint[] spawnPoints = currentLevel.GetComponentsInChildren<SpawnPoint>();

         int enemies = spawnPoints.Length;
        for(int i = 0; i < spawnPoints.Length; i++) {
            if(Random.Range(0f, 1f) > difficulty) {
                Destroy(spawnPoints[i].gameObject);
                enemies--;
            }
        }
        currentLevel.Enemies = enemies;
        return currentLevel;
    }

    private void Generate() {
        _floor = new GameObject();
        GenerateWalls();
        //GenerateFloor();
        GenerateTeleports();
    }


    private void GenerateFloor() {
        for(float x = -(dimensions.x - 3) / 2; x <= (dimensions.x - 3) / 2; x++) {
            for(float y = -(dimensions.y - 3) / 2; y <= (dimensions.y - 3) / 2; y++) {
                GameObject floorPiece = Instantiate(floor[0], _floor.transform);
                floorPiece.transform.localPosition = new Vector2(x, y);
            }
        }
    }

    private void GenerateWalls() {
        for(int i = 0; i < 4; i++) {
            //Place Corners
            GameObject corner = Instantiate(corners[i], _floor.transform);
            corner.transform.localPosition = CombineVectors(directions[i] + directions[(i + 1) % 4], (dimensions - Vector2.one) / 2);


            //Place Walls
            Vector2 wallPos = CombineVectors(directions[i], (dimensions - Vector2.one) / 2);
            Vector2 wallDirection = directions[(i + 1) % 4];
            int wallLength = (int) CombineVectors(wallDirection, dimensions).magnitude / 2 - 1;
            for(int j = -wallLength; j <= wallLength; j++) {
                GameObject wall = Instantiate(walls[i], _floor.transform);
                wall.transform.localPosition = wallPos + j * wallDirection;
            }

        }
    }


    public void reBuild() {
        foreach(Transform child in transform) {
            child.gameObject.SetActive(true);
        }
        Generate();

    }

    public void Delete() {
        foreach(Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        Destroy(_floor.gameObject);

    }

    void GenerateTeleports() {
        foreach(Vector2 direction in directions) {
            Teleporter tele = Instantiate(teleporterPrefab, _floor.transform);
            tele.level = this;
            tele.direction = direction;
            tele.transform.localPosition = CombineVectors(direction, (dimensions - Vector2.one) / 2);
        }
    }

    public void EnterTeleporter(Vector2 direction) {
        //Vector2 pos = CombineVectors(direction, dimensions) + position;
        Vector2 pos = direction + position;
        LevelManager.GetLevelManager().EnterRoom(direction, pos);
    }

    private Vector2 CombineVectors(Vector2 a, Vector2 b) {
        return new Vector2(a.x * b.x, a.y * b.y);
    }

    public override String ToString() {
        return "Level: " + position.ToString();
    }

    public Vector2 getTeleportPosition(Vector2 direction) {
        return CombineVectors(direction, (dimensions - Vector2.one) / 2);
    }
}
