using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : Item {
    [SerializeField] private BulletModifier upgrade;

    void Start() {
        Debug.Log("Weapon Upgrade start");
        text = upgrade.GetToolTip();
    }
    public override void pickedUp(PlayerController playerController) {
        playerController.addBulletModifier(upgrade);
        base.pickedUp(playerController);
        
    }
}
