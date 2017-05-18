using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletModifier : MonoBehaviour {

    public abstract string GetToolTip();

    public virtual B OnFireBullet<B>(B b) where B:Bullet {
        return b;
    }

}
