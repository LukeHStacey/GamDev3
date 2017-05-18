using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet {
    public override void Start() {
        tagToHit = "Enemy";
        base.Start();
    }

}
