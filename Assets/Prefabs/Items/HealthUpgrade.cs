﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : Item{

    void Start() {
        text = "+ Max Health";
    }
    public override void pickedUp(PlayerController playerController) {
        playerController.MaxHealth = playerController.MaxHealth + 2;
        playerController.Health = playerController.Health + 2;
        base.pickedUp(playerController);
    }
}
