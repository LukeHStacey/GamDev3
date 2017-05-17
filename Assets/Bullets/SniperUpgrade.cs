using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperUpgrade : BulletModifier{
    
    public override B OnFireBullet<B>(B b) {
        b.damage = b.damage * 4;
        b.speed = b.speed * 4;
        b.transform.localScale = 0.5f * b.transform.localScale;

        return b;
    }

    public override void OnApply(Character c) {
        c.shootDelay = c.shootDelay * 2;
    }
}
