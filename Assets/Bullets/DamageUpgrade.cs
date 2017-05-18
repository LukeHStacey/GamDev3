using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : BulletModifier {

    public override B OnFireBullet<B>(B b) {
        b.damage = b.damage + 0.5f;
        return b;
    }

    public override string GetToolTip() {
        return "+ Damage";
    }
}
