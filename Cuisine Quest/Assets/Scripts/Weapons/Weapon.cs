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
}
