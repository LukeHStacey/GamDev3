using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Player")) {
            pickedUp(PlayerController.GetPlayerController());
        }
    }

    public virtual void pickedUp(PlayerController playerController) {
     Destroy(gameObject);   
    }
}
