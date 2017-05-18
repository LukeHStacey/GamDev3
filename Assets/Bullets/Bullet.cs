using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public float damage;
    private Vector2 lastpos;
    private float SpawnTime;
    public float LifeTime = 5;
    public bool isLine;
    public bool canBounce;
    public float reloadTime;

    public String tagToHit;

    private int sortingLayer;
    private int wallMask;
    private int enemyMask;

    private Color color;

    public Vector2 direction { get; private set; }

	// Use this for initialization
    void Awake() {
        tagToHit = "Player";
    }
	public virtual void Start () {
        transform.localPosition = Vector2.zero;
	    wallMask = 1 << LayerMask.NameToLayer("Inner Walls") | 1 << LayerMask.NameToLayer("Outer Walls");
        enemyMask = 1 << LayerMask.NameToLayer("Enemies");
	    sortingLayer = GetComponent<SpriteRenderer>().sortingLayerID;
	    color = GetComponent<SpriteRenderer>().color;
	}

    public static B FireBullet<B>(Vector2 direction, Transform Shooter, B prefab) where B:Bullet {
        direction.Normalize();
        B bullet = Instantiate(prefab, Shooter);
        bullet.lastpos = (Vector2) Shooter.position + direction/3;
        bullet.direction = direction;
        bullet.SpawnTime = Time.time;
        return bullet;
    }
	
	// Update is called once per frame
	void Update () {
	    if (isLine) {
	        RaycastHit2D wall = Physics2D.Raycast(lastpos, direction, float.MaxValue, wallMask);
	        if (wall) {
	            RaycastHit2D[] raycasts = Physics2D.RaycastAll(lastpos, direction, wall.distance, enemyMask);
	            foreach (RaycastHit2D hit2D in raycasts) {
	                hit2D.collider.GetComponent<Character>().hitBy(this);
	            }
	            DrawLine(lastpos, lastpos + direction * wall.distance, color, reloadTime/2);
	        }
	        Destroy(gameObject);

            return;
	    }
	    if (Time.time > SpawnTime + LifeTime) {
	        Destroy(gameObject);
	    }
	    else {
	        transform.position = Vector2.Lerp(lastpos, lastpos + direction, speed * Time.deltaTime);
	        lastpos = transform.position;
	    }

	}

    private Vector2[] directions = new Vector2[4] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other);
        if (other.tag.Equals(tagToHit)) {
            other.GetComponent<Character>().hitBy(this);

            Destroy(gameObject);
        }else if (other.tag.Equals("Wall")) {
            if ( canBounce) {
                foreach (Vector2 cardinal in directions) {
                    RaycastHit2D ray = Physics2D.Raycast(lastpos, direction, float.MaxValue, wallMask);
                    if (ray && ray.collider == other) {
                        Vector2 newDirection = new Vector2(-Mathf.Abs(cardinal.x), -Mathf.Abs(cardinal.y));
                        direction = CombineVectors(newDirection, direction);
                    }
                    else {
                        Debug.Log(ray.collider);
                        Debug.DrawLine(lastpos, ray.point, Color.red, 1f);
                        Debug.DrawLine(lastpos, lastpos *0.9f, Color.green, 1f);
                    }
                }
            }
            else {
                Destroy(gameObject);
            }
        }
    }


    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f) {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.sortingLayerID = sortingLayer;
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

    private Vector2 CombineVectors(Vector2 a, Vector2 b) {
        return new Vector2(a.x * b.x, a.y * b.y);
    }
}
