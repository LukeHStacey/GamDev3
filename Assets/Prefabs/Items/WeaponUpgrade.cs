using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : Item {
    [SerializeField] private BulletModifier upgrade;
    public override void pickedUp(PlayerController playerController) {
        playerController.addBulletModifier(upgrade);
        base.pickedUp(playerController);
    }
}
