using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBullet : ShapeModifier {

    public override List<B> OnFireBullet<B>(List<B> bulletList) {
        List<Bullet> newBullets = new List<Bullet>();

        foreach (B bullet in bulletList) {
            
        Vector2 direction2 = Quaternion.Euler(0, 0, 15) * bullet.direction;
        Vector2 direction3 = Quaternion.Euler(0, 0, -15) * bullet.direction;

        bullet.reloadTime = bullet.reloadTime * 2;
        Bullet.FireBullet(direction2, bullet.transform.parent, bullet);
        Bullet.FireBullet(direction3, bullet.transform.parent, bullet);

        }
        return bulletList;
    }
    public override string GetToolTip() {
        return "+ Shots Fired";
    }

    public override int GetPriority() {
        return 100;
    }
}
