using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    private Vector2 lastpos;

    public Vector2 direction {
        get { return _direction; }
        set { _direction = value.normalized; } 
    }

    private Vector2 _direction;
	// Use this for initialization
	void Start () {
	    lastpos = transform.parent.position;
	}
	
	// Update is called once per frame
	void Update () {
	    transform.position = Vector2.Lerp(lastpos, lastpos +  direction, speed * Time.deltaTime);
	    lastpos = transform.position;
	}


    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.name);
        if (other.tag.Equals("Player")) {
            Debug.Log("Hit Player!");
            Destroy(gameObject);
        }else if (other.tag.Equals("Wall")) {
            Debug.Log("Hit Wall!", other.gameObject );
            Destroy(gameObject);
        }
    }

}
