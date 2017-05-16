using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 3;
    public float shootDelay;
    private float lastShotTime;
    [SerializeField]
    private PlayerBullet bulletPrefab;

	static PlayerController _PlayerController;

	void Awake(){
		PlayerController._PlayerController = this;
	}


	// Use this for initialization
	void Start () {
	    lastShotTime = -shootDelay;
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();
		Vector2 velocity = new Vector2 ();
		if (Input.GetKey(KeyCode.A)){
			velocity.x = -1;
		}else if (Input.GetKey(KeyCode.D)){
			velocity.x = +1;
		}
		if (Input.GetKey(KeyCode.W)){
			velocity.y = 1;
		}else if (Input.GetKey(KeyCode.S)){
			velocity.y = -1;
		}
		rigidBody.velocity = velocity * speed;

	    if (lastShotTime + shootDelay < Time.time) {
	        Vector2 bulletDirection = new Vector2();
	        bool shooting = false;
	        if (Input.GetKey(KeyCode.LeftArrow)) {
	            bulletDirection.x = -1;
	            shooting = true;
	        }
	        else if (Input.GetKey(KeyCode.RightArrow)) {
	            bulletDirection.x = +1;
	            shooting = true;
	        }
	        if (Input.GetKey(KeyCode.UpArrow)) {
	            bulletDirection.y = 1;
	            shooting = true;
	        }
	        else if (Input.GetKey(KeyCode.DownArrow)) {
	            bulletDirection.y = -1;
	            shooting = true;
	        }

	        if (shooting) {
	            lastShotTime = Time.time;
	            PlayerBullet bullet = Instantiate(bulletPrefab, transform);
                bullet.transform.localPosition = Vector3.zero;
	            bullet.direction = bulletDirection + velocity;
	        }
	    }
	}

	public static PlayerController GetPlayerController(){
		return _PlayerController;
	}
}
