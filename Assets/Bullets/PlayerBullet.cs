using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Enemy")) {
            Debug.Log("Hit Bad guy!");
            other.GetComponent<Character>().hitBy(this);
            Destroy(gameObject);
        }else if (other.tag.Equals("Wall")) {
            Destroy(gameObject);
        }
    }
}
