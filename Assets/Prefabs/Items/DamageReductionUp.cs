using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReductionUp : Item{
    void Start() {
        text = "+ Armour";
    }

    public override void pickedUp(PlayerController playerController) {
        playerController.damageReduction += 0.5f;
        base.pickedUp(playerController);
    }

}
