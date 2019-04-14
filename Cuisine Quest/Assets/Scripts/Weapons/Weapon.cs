using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public int WeaponPower = 1;

    public abstract void AttackRight();
    public abstract void AttackLeft();
    public abstract void AttackDown();
    public abstract void AttackUp();
}
