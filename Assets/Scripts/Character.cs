using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    public float health;
    public float LastHit = 0;
    protected float flashTime = 0.2f;

    public void OnUpdate() {
        if(LastHit + flashTime < Time.time) {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void hitBy(Bullet bullet) {
        takeDamage(bullet.damage);
    }


    public virtual void takeDamage(int damage) {
        if (LastHit + flashTime < Time.time) {
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
