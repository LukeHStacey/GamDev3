using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperUpgrade : BulletModifier{
    
    public override B OnFireBullet<B>(B b) {
        b.damage = b.damage * 4;
        b.speed = b.speed * 4;
        b.transform.localScale = 0.5f * b.transform.localScale;
        b.reloadTime = b.reloadTime * 2;

        return b;
    }

    public override string GetToolTip() {
        return "+ Sniper Mode";
    }
}
