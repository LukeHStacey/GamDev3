using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalShot: ShapeModifier{

    public override List<B> OnFireBullet<B>(List<B> bulletList) {
        List<B> newBullets = new List<B>();

        foreach (B bullet in bulletList) {
            
        Vector2 direction2 = Quaternion.Euler(0, 0, 90) * bullet.direction;
        Vector2 direction3 = Quaternion.Euler(0, 0, 180) * bullet.direction;
        Vector2 direction4 = Quaternion.Euler(0, 0, 270) * bullet.direction;

        bullet.damage = bullet.damage*2/3;
        newBullets.Add(Bullet.FireBullet(direction2, bullet.transform.parent, bullet));
        newBullets.Add(Bullet.FireBullet(direction3, bullet.transform.parent, bullet));
        newBullets.Add(Bullet.FireBullet(direction4, bullet.transform.parent, bullet));

        }
        newBullets.AddRange(bulletList);
        return newBullets;
    }
    public override string GetToolTip() {
        return "+ Many Shots Fired";
    }

    public override int GetPriority() {
        return 50;
    }
}
