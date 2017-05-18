using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {
    protected string text;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Player")) {
            pickedUp(PlayerController.GetPlayerController());
        }
    }

    public virtual void pickedUp(PlayerController playerController) {
        UIManager.Manager.UpdateTooltip(text, 2f);
     Destroy(gameObject);   
    }
}
