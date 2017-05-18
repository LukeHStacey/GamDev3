using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateOfFireMod : BulletModifier{

    public override B OnFireBullet<B>(B b) {
        b.reloadTime = b.reloadTime * 0.8f;
        return b;
    }
    public override string GetToolTip() {
        return "+ Rate of Fire";
    }
}
