using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBullet : ShapeModifier {

    public override List<B> OnFireBullet<B>(List<B> bulletList) {
        List<B> newBullets = new List<B>();

        foreach(B bullet in bulletList) {
            for(int i = -numberCollected; i <= numberCollected; i++) {


                Vector2 direction2 = Quaternion.Euler(0, 0, i * 15) * bullet.direction;
                Vector2 direction3 = Quaternion.Euler(0, 0, i * -15) * bullet.direction;

                bullet.reloadTime = bullet.reloadTime * 2;
                newBullets.Add(Bullet.FireBullet(direction2, bullet.transform.parent, bullet));
                newBullets.Add(Bullet.FireBullet(direction3, bullet.transform.parent, bullet));
            }

        }
        newBullets.AddRange(bulletList);
        return newBullets;
    }
    public override string GetToolTip() {
        return "+ Shots Fired";
    }

    public override int GetPriority() {
        return 100;
    }
}
