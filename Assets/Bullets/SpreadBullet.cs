using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBullet : BulletModifier {

    public override B OnFireBullet<B>(B b) {

        Vector2 direction2 = Quaternion.Euler(0, 0, 15) * b.direction;
        Vector2 direction3 = Quaternion.Euler(0, 0, -15) * b.direction;

        Bullet.FireBullet(direction2, b.transform.parent, b);
        Bullet.FireBullet(direction3, b.transform.parent, b);
        return b;
    }

    public override void OnApply(Character c) {
        c.shootDelay = c.shootDelay * 2;
    }
}
