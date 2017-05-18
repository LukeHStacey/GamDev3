using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedIncrease : Item{

    void Start() {
        text = "+ Move Speed";
    }
    public override void pickedUp(PlayerController playerController) {
        playerController.speed += 1;
        base.pickedUp(playerController);
    }

    // Use this for initialization
}
