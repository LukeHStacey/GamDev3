using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShapeModifier : MonoBehaviour {
    public int numberCollected;

    void Start() {
        numberCollected = 1;
    }
    
    public virtual List<B> OnFireBullet<B>(List<B> b) where B:Bullet {
        return b;
    }
    public abstract string GetToolTip();
    public abstract int GetPriority();
}
