using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    public float health;
    public float LastHit = 0;
    private float flashTime = 0.1f;

    public void OnUpdate() {
	    if (LastHit + flashTime < Time.time) {
            GetComponent<SpriteRenderer>().color = Color.white;
	    }
    }

    public void hitBy(Bullet bullet) {
        health -= bullet.damage;
        if (health <= 0) {
            this.Die();
        }
        else {
            GetComponent<SpriteRenderer>().color = Color.red;
            LastHit = Time.time;
        }
    }

    public abstract void Die();
}
