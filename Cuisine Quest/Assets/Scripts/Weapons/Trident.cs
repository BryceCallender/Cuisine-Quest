using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident : Weapon {
    public JabbingWeapon jw;
    
    private bool Jabbing = false;
    private bool HoldAtAttack = false;

    public float ProjectileSpeed = 10;
    public float ProjectileFireRate = 1;
    private float lastProjectileFire = float.NegativeInfinity;
    public TridentProjectile Tridentin;

    //public GameObject Mesh;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        HoldAtAttack = Input.GetMouseButton(0);
        jw.JabAttack(transform, ref Jabbing, HoldAtAttack);

    }

    private void attackSecondary()
    {
        //Debug.Log("Fire");
        GameObject projectile = Instantiate(Tridentin.gameObject, Tridentin.transform.position, Quaternion.identity);
        projectile.SetActive(true);
        projectile.transform.right = transform.right;
        projectile.transform.parent = null;
        projectile.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * transform.right;
    }

    public override void Attack(Vector2 PlayerDirection)
    {
        if (jw.CanAttack())
        {
            if (PlayerDirection.x < 0) jw.AttackLeft(transform, ref Jabbing);
            else if (PlayerDirection.x > 0) jw.AttackRight(transform, ref Jabbing);
            else if (PlayerDirection.y < 0) jw.AttackDown(transform, ref Jabbing);
            else if (PlayerDirection.y > 0) jw.AttackUp(transform, ref Jabbing);
        }
        
    }
    public override void AttackSecondary(Vector2 PlayerDirection, bool PrimaryAttacking)
    {
        if (PrimaryAttacking) attackSecondary();
    }

    
    private void attackEnd()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        Mesh.SetActive(false);
        Jabbing = false;
    }

    public override bool AttackAbort()
    {
        if (!HoldAtAttack)
        {
            attackEnd();
        }
        return !HoldAtAttack;
    }

    public override void AttackAbortForced()
    {
        attackEnd();
    }
}
