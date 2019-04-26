using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public int WeaponPower = 1;

    public abstract void Attack(Vector2 PlayerDirection);
    public abstract void AttackSecondary(Vector2 PlayerDirection, bool PrimaryAttacking);

    public abstract bool AttackAbort();
    public abstract void AttackAbortForced();

    public GameObject Mesh;

    protected BoxCollider2D weaponCollider;

    void Start()
    {
        weaponCollider = GetComponent<BoxCollider2D>();
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthSystem health = collision.GetComponent<HealthSystem>();
        if (health)
        {
            health.takeDamage(WeaponPower);
        }
    }
}
