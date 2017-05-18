using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserUpgrde : BulletModifier{
    void Awake() {
    }
    public override B OnFireBullet<B>(B b) {
        b.damage = (int)Mathf.Ceil(b.damage * 0.5f);
        b.reloadTime = b.reloadTime * 0.5f;
        b.isLine = true;

        return b;
    }
    public override string GetToolTip() {
        return "+ Lasers";
    }
}
