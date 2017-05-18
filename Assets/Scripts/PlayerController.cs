using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : Character{
    [SerializeField]
    private int maxHealth = 10;
    private Healthbar healthBar;


	static PlayerController _PlayerController;
    private bool dead;

    public int MaxHealth {
        get { return maxHealth; }
         set { maxHealth = value; healthBar.UpdateHealth(health, maxHealth); }
    }

    public float Health {
        get { return health; }
         set { health = Mathf.Min(value, MaxHealth); healthBar.UpdateHealth(health, maxHealth); }
    }

    void Awake(){
		PlayerController._PlayerController = this;
	}


	// Use this for initialization
    public override void  Start () {
        base.Start();
	    dead = false;
        healthBar = Healthbar.HPBar;
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
                    Shoot(bulletDirection);
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
    }

    protected override void onTakeDamage(float amount) {
        base.onTakeDamage(amount);
        healthBar.UpdateHealth(health, MaxHealth);
    }


    public void addBulletModifier(BulletModifier modifier) {
        bulletModifiers.Add(modifier);
    }

    public void OnEnterRoom() {
        foreach(Transform child in transform) {
            Destroy(child.gameObject);           
        }
    }

    public void addShapeModifier(ShapeModifier shapeUpgrade) {
        if (!shapeModifiers.ContainsKey(shapeUpgrade.GetPriority())) {
            shapeModifiers.Add(shapeUpgrade.GetPriority(), shapeUpgrade);
        }
    }
}
