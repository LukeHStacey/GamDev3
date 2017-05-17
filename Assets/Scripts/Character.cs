using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField]
    protected int health;
    public float LastHit = 0;
    protected float flashTime = 0.2f;
    public float shootDelay;
    public float speed;
    [SerializeField]
    protected Bullet bulletPrefab;
    [SerializeField] protected List<BulletModifier> modifiers;

    void Start() {
        foreach (BulletModifier bulletModifier in modifiers) {
            bulletModifier.OnApply(this);
        }
    }

    public void OnUpdate() {
        if(LastHit + flashTime < Time.time) {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void hitBy(Bullet bullet) {
        takeDamage(bullet.damage);
    }


    public virtual void takeDamage(int damage) {
        if (LastHit + flashTime < Time.time && damage > 0) {
            onTakeDamage(damage);
        }
    }

    protected virtual void onTakeDamage(int amount) {
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
