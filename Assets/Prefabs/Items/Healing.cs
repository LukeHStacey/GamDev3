﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : Item{

    void Start() {
        text = "+ Health";
    }
    public override void pickedUp(PlayerController playerController) {
        playerController.Health = playerController.Health + 5;
        base.pickedUp(playerController);
    }
}
