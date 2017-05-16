using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : Character{
    public float speed;
    [SerializeField]
    private float shootDelay;
    [SerializeField] private Bullet bullet;
    // Use this for initialization
	void Start () {
	    LastShotTime = Time.time;
	}

    public float LastShotTime { get; set; }

    // Update is called once per frame
	void Update () {
	    base.OnUpdate();
	    PlayerController player = PlayerController.GetPlayerController();
	    Vector2 direction = (player.transform.position - transform.position);
	    if (direction.magnitude > 0.1) {
	        Rigidbody2D body = GetComponent<Rigidbody2D>();
	        body.velocity = speed * (player.transform.position - transform.position).normalized;
	    }


	    if (LastShotTime + shootDelay < Time.time) {
	        LastShotTime = Time.time;
	        Bullet shot = Instantiate(bullet, transform);
            shot.transform.localPosition = Vector3.zero;
	        shot.direction = (PlayerController.GetPlayerController().transform.position - transform.position).normalized * bullet.speed;
	    }

	}
    public override void Die() {
        Destroy(transform.parent.gameObject);
    }

}
