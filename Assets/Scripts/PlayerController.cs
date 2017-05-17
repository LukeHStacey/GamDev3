using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : Character{
	public float speed = 3;
    public float shootDelay;
    private float lastShotTime;
    private int maxhealth = 10;
    [SerializeField]
    private PlayerBullet bulletPrefab;
    private List<BulletModifier> modifiers;

    [SerializeField] private Slider healthBar;


	static PlayerController _PlayerController;
    private bool dead;

    void Awake(){
		PlayerController._PlayerController = this;
        modifiers = new List<BulletModifier>();
	}


	// Use this for initialization
	void Start () {
	    lastShotTime = -shootDelay;
	    dead = false;
	    healthBar.maxValue = maxhealth;
	    healthBar.minValue = 0;
	    healthBar.value = health;
	}
	
	// Update is called once per frame
	void Update () {
	        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
	    if (!dead) {
            base.OnUpdate();
	        Vector2 velocity = new Vector2();
	        if (Input.GetKey(KeyCode.A)) {
	            velocity.x = -1;
	        }
	        else if (Input.GetKey(KeyCode.D)) {
	            velocity.x = +1;
	        }
	        if (Input.GetKey(KeyCode.W)) {
	            velocity.y = 1;
	        }
	        else if (Input.GetKey(KeyCode.S)) {
	            velocity.y = -1;
	        }
	        rigidBody.velocity = velocity * speed;

	        if (lastShotTime + shootDelay < Time.time) {
	            Vector2 bulletDirection = new Vector2();
	            bool shooting = false;
	            if (Input.GetKey(KeyCode.LeftArrow)) {
	                bulletDirection.x = -1;
	                bulletDirection.y = velocity.y;
	                shooting = true;
	            }
	            else if (Input.GetKey(KeyCode.RightArrow)) {
	                bulletDirection.x = +1;
	                bulletDirection.y = velocity.y;
	                shooting = true;
	            }else if (Input.GetKey(KeyCode.UpArrow)) {
	                bulletDirection.y = 1;
	                bulletDirection.x = velocity.x;
	                shooting = true;
	            }
	            else if (Input.GetKey(KeyCode.DownArrow)) {
	                bulletDirection.y = -1;
	                bulletDirection.x = velocity.x;
	                shooting = true;
	            }

	            if (shooting) {
	                lastShotTime = Time.time;
	                PlayerBullet bullet = Bullet.FireBullet(bulletDirection, transform, bulletPrefab);
	                foreach (BulletModifier bulletModifier in modifiers) {
	                    bullet = bulletModifier.OnFireBullet(bullet);
	                }
	            }
	        }
	    }
	    else {
	        rigidBody.velocity = Vector2.zero;
            GetComponent<SpriteRenderer>().color = Color.red;
	        if (Input.GetKeyDown(KeyCode.R)) {
	            SceneManager.LoadScene(0);
	        }
	    }
	}

	public static PlayerController GetPlayerController(){
		return _PlayerController;
	}

    public override void Die() {
        dead = true;
        healthBar.fillRect.GetComponent<Image>().color = Color.red;
    }

    protected override void onTakeDamage(int amount) {
        healthBar.value = health;
        base.onTakeDamage(amount);
    }

    public void addBulletModifier(BulletModifier modifier) {
        modifiers.Add(modifier);
    }

}
