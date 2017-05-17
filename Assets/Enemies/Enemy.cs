using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : Character {
    public float speed;
    [SerializeField]
    private float shootDelay;
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private int meleeDamage;
    [SerializeField]
    private BulletModifier[] _modifiers;
    private List<BulletModifier> modifiers;
    private int RaycastMask;
    // Use this for initialization
    void Start() {
        LastShotTime = Time.time;
        flashTime = 0.1f;
        RaycastMask = 1 << LayerMask.NameToLayer("Inner Walls") | 1 << LayerMask.NameToLayer("Outer Walls") |
                      1 << LayerMask.NameToLayer("Player");

        modifiers = new List<BulletModifier>(_modifiers.Length);
        foreach(BulletModifier modifier in _modifiers) {
            modifiers.Add(modifier);
        }
    }

    public float LastShotTime { get; set; }

    // Update is called once per frame
    void Update() {
        base.OnUpdate();
        PlayerController player = PlayerController.GetPlayerController();
        Vector2 direction = (player.transform.position - transform.position);
        if(direction.magnitude > 0.1) {
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            body.velocity = speed * (player.transform.position - transform.position).normalized;
        }

        if(bullet != null) {
            if(LastShotTime + shootDelay < Time.time) {
                RaycastHit2D raycast = Physics2D.Raycast(transform.position, player.transform.position - transform.position,
                    float.MaxValue, RaycastMask);
                Debug.DrawLine(transform.position, raycast.point, Color.red);
                Debug.Log(raycast.collider.name);
                if(raycast.collider.tag.Equals("Player")) {
                    LastShotTime = Time.time;
                    Vector2 shotDirection =
                        (PlayerController.GetPlayerController().transform.position - transform.position).normalized *
                        bullet.speed;
                    Bullet shot = Bullet.FireBullet(shotDirection, transform, bullet);
                    foreach(BulletModifier modifier in modifiers) {
                        shot = modifier.OnFireBullet(shot);
                    }
                }
            }
        }

    }
    public override void Die() {
        GetComponentInParent<Level>().Enemies--;
        Destroy(transform.parent.gameObject);
    }

    public void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag.Equals("Player")) {
            PlayerController.GetPlayerController().takeDamage(meleeDamage);
        }
    }

}
