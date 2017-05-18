using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : Item {
    [SerializeField] private BulletModifier bulletUpgrade;
    [SerializeField] private ShapeModifier ShapeUpgrade;

    void Start() {
        Debug.Log("Weapon Upgrade start");
        if (bulletUpgrade != null) {
            text = bulletUpgrade.GetToolTip();
        }
        else {
            text = ShapeUpgrade.GetToolTip();
        }
    }

    public override void pickedUp(PlayerController playerController) {
        if (bulletUpgrade != null) {
            playerController.addBulletModifier(bulletUpgrade);
        }
        if (ShapeUpgrade != null)
            playerController.addShapeModifier(ShapeUpgrade);
        base.pickedUp(playerController);
        
    }
}
