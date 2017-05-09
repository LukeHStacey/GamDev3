using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	[SerializeField] private Teleporter teleporterPrefab;
	[SerializeField] public float size; 
	public Level prefab {get; set;}
	public Vector2 position { get; set; }

	private Vector2[] directions = new Vector2[4]{ Vector2.up, Vector2.down, Vector2.left, Vector2.right };
	// Use this for initialization
	void Start () {
		GenerateTeleports ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnterTeleporter (Vector2 direction)
	{
		LevelManager.GetLevelManager ().EnterRoom (direction, position);
	}

	public void reBuild ()
	{
		foreach (Transform child in prefab.transform) {
			Instantiate(child.gameObject, transform);
		}
		GenerateTeleports ();

	}

	public void Delete ()
	{
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}

	}

	void GenerateTeleports ()
	{
		foreach (Vector2 direction in directions) {
			Teleporter tele = Instantiate (teleporterPrefab, this.transform);
			tele.level = this;
			tele.direction = direction;
		}
	}
}
