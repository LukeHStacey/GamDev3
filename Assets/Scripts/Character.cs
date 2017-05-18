using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    protected float lastShotTime;
    [SerializeField]
    protected float health;
    public float LastHit = 0;
    protected float flashTime = 0.2f;
    public float shootDelay;
    public float speed;
    public float damageReduction;
    [SerializeField]
    protected Bullet bulletPrefab;
    [SerializeField]
    protected List<BulletModifier> bulletModifiers;
    [SerializeField]
    protected SortedList<int, ShapeModifier> shapeModifiers;

    public virtual void Start() {
        shootDelay = 0;
        shapeModifiers = new SortedList<int, ShapeModifier>();
    }

    public void OnUpdate() {
        if(LastHit + flashTime < Time.time) {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void hitBy(Bullet bullet) {
        takeDamage(bullet.damage);
    }


    public virtual void takeDamage(float damage) {
        damage = damage - damageReduction;
        if(LastHit + flashTime < Time.time && damage > 0) {
            onTakeDamage(damage);
        }
    }

    protected virtual void onTakeDamage(float amount) {
        health -= amount;
        if(health <= 0) {
            this.Die();
        }
        else {
            GetComponent<SpriteRenderer>().color = Color.red;
            LastHit = Time.time;
        }
    }

    protected void Shoot(Vector2 direction) {
        lastShotTime = Time.time;
        Bullet bullet = Bullet.FireBullet(direction, transform, bulletPrefab);
        foreach(BulletModifier bulletModifier in bulletModifiers) {
            bullet = bulletModifier.OnFireBullet(bullet);
        }
        List<Bullet> bullets = new List<Bullet>();
        bullets.Add(bullet);
        foreach(KeyValuePair<int, ShapeModifier> valuePair in shapeModifiers) {
            bullets = valuePair.Value.OnFireBullet(bullets);
        }
        shootDelay = bullets[0].reloadTime;
    }

    public abstract void Die();
}
