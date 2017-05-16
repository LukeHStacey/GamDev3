﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Vector2 dimensions {
        get { return new Vector2(x, y); }
        set {
            x = value.x;
            y = value.y;
        }
    }
    public Level prefab { get; set; }
    public Vector2 position { get; set; }

    private Vector2[] directions = new Vector2[4] { Vector2.up, Vector2.right, Vector2.down, Vector2.left};
    // Use this for initialization
    void Start() {
        GenerateLevel();
    }

    private void GenerateLevel() {
        GenerateWalls();
        GenerateFloor();
        GenerateTeleports();
    }

    private void GenerateFloor() {
        for (float x = -(dimensions.x - 3) / 2; x <= (dimensions.x - 3) / 2; x++) {
            for (float y = -(dimensions.y - 3) / 2; y <= (dimensions.y - 3) / 2; y++) {
                GameObject floorPiece = Instantiate(floor[0], transform);
                floorPiece.transform.localPosition = new Vector2(x, y);
            }
        }
    }

    private void GenerateWalls() {
        for (int i = 0; i < 4; i++) {
            //Place Corners
            GameObject corner = Instantiate(corners[i], transform);
            corner.transform.localPosition = CombineVectors(directions[i] + directions[(i+1)%4], (dimensions - Vector2.one)/2);


            //Place Walls
            Vector2 wallPos = CombineVectors(directions[i], (dimensions - Vector2.one)/2);
            Vector2 wallDirection = directions[(i + 1) % 4];
            int wallLength = (int) wallPos.magnitude - 1;
            for (int j = -wallLength; j <= wallLength; j++) {
                GameObject wall = Instantiate(walls[i], transform);
                wall.transform.localPosition = wallPos + j * wallDirection;
            }

        }
    }


    public void reBuild() {
        foreach(Transform child in prefab.transform) {
            Instantiate(child.gameObject, transform);
        }
        GenerateFloor();
        GenerateWalls();
        GenerateTeleports();

    }

    public void Delete() {
        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }

    }

    void GenerateTeleports() {
        foreach(Vector2 direction in directions) {
            Teleporter tele = Instantiate(teleporterPrefab, this.transform);
            tele.level = this;
            tele.direction = direction;
            tele.transform.localPosition = CombineVectors(direction, (dimensions - Vector2.one)/2);
        }
    }

    public void EnterTeleporter(Vector2 direction) {
        Vector2 pos = CombineVectors(direction, dimensions) + position;
        LevelManager.GetLevelManager().EnterRoom(direction, pos);
    }

    private Vector2 CombineVectors(Vector2 a, Vector2 b) {
        return new Vector2(a.x * b.x, a.y * b.y);
    }

    public override String ToString() {
        return "Level: " + position.ToString();
    }
}