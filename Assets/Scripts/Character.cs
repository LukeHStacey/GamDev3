using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField]
    protected float health;
    public float LastHit = 0;
    protected float flashTime = 0.2f;
    public float shootDelay;
    public float speed;
    public float damageReduction;
    [SerializeField]
    protected Bullet bulletPrefab;
    [SerializeField] protected List<BulletModifier> bulletModifiers;
    [SerializeField] protected List<ShapeModifier> ShapeModifiers;

    void Start() {
        shootDelay = 0; 
    }

    public void OnUpdate() {
        if(LastHit + flashTime < Time.time) {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void hitBy(Bullet bullet) {
        takeDamage(bullet.damage);
    }


    public virtual void takeDamage(float damage) {
        damage = damage - damageReduction;
        if (LastHit + flashTime < Time.time && damage > 0) {
            onTakeDamage(damage);
        }
    }

    protected virtual void onTakeDamage(float amount) {
        health -= amount;
        if(health <= 0) {
            this.Die();
        }
        else {
            GetComponent<SpriteRenderer>().color = Color.red;
            LastHit = Time.time;
        }
    }

    public abstract void Die();
}
