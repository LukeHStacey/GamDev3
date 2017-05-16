using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.name);
        if (other.tag.Equals("Enemy")) {
            Debug.Log("Hit Bad guy!");
            Destroy(gameObject);
        }else if (other.tag.Equals("Wall")) {
            Debug.Log("Hit Wall!", other.gameObject );
            Destroy(gameObject);
        }
    }
}
