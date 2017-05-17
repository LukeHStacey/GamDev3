using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public int damage;
    private Vector2 lastpos;


    public Vector2 direction { get; set; }

	// Use this for initialization
	void Start () {
        transform.localPosition = Vector2.zero;
	}

    public static B FireBullet<B>(Vector2 direction, Transform Shooter, B prefab) where B:Bullet {
        direction.Normalize();
        B bullet = Instantiate(prefab, Shooter);
        bullet.lastpos = (Vector2) Shooter.position + direction/3;
        bullet.direction = direction* bullet.speed;
        return bullet;
    }
	
	// Update is called once per frame
	void Update () {
	    transform.position = Vector2.Lerp(lastpos, lastpos +  direction,  Time.deltaTime);
	    lastpos = transform.position;
	}


    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Player")) {
            other.GetComponent<Character>().hitBy(this);

            Debug.Log("Hit Player!");
            Destroy(gameObject);
        }else if (other.tag.Equals("Wall")) {
            Destroy(gameObject);
        }
    }

}
