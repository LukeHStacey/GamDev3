using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager Manager { get; private set; }
    [SerializeField]
    private Enemy[] Enimies;

    void Awake() {
        EnemyManager.Manager = this;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Enemy GetEnemyByDifficulty(float difficulty) {
        return Enimies[GetObjectByDifficulty(difficulty, Enimies.Length)];
    }

    private int GetObjectByDifficulty(float difficulty, int length) {
        //Number of room until gaurenteed Boss
        float lowerLim = (difficulty * (length));
        return (int) Mathf.Clamp(Mathf.Floor(Random.Range(0, lowerLim)), 0, Enimies.Length-1);
    }

}
