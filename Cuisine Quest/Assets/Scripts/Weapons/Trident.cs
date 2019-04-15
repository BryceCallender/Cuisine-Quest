﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident : Weapon {
    public float AttackWait = 0.5f;
    private float attackedLast = float.NegativeInfinity;

    public float JabSpeed = 100;
    public float JabDistance = 1.0f;
    public float JabDuration = 0.2f;

    public float JabRightDistance = 0.5f;
    public float JabLeftDistance = 0.5f;
    public float JabUpDistance = 0.75f;
    public float JabDownDistance = 0.75f;

    public float JabXOffset = 0.1f;
    public float JabYOffset = 0.1f;

    private Vector2 JabVector;

    private float JabFinish;
    private bool jabFinishSet = false;
    private bool Jabbing = false;
    private bool HoldAtAttack = false;

    public float ProjectileSpeed = 10;
    public float ProjectileFireRate = 1;
    private float lastProjectileFire = float.NegativeInfinity;
    public TridentProjectile Tridentin;

    public GameObject Mesh;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        HoldAtAttack = Input.GetMouseButton(0);

        if (Jabbing && Vector3.Magnitude(transform.localPosition) < Vector3.Magnitude(JabVector))//JabDistance)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, JabVector, JabSpeed * Time.deltaTime);
            //Debug.Log(Vector3.Magnitude(transform.localPosition));
        }
        else if(Jabbing && HoldAtAttack)
        {
            
            if (!jabFinishSet)
            {
                JabFinish = Time.fixedTime;
                jabFinishSet = true;
            }
        }
        else if (Jabbing)
        {
            Jabbing = false;
            if (!jabFinishSet)
            {
                JabFinish = Time.fixedTime;
                jabFinishSet = true;
            }
        }
        else if (JabFinish + JabDuration < Time.fixedTime)
        {
            AttackAbort();
        }
        //Debug.Log(JabFinish);
    }

    private void attack()
    {
        //JabFinish = Time.fixedTime + 2;
        if (attackedLast + AttackWait < Time.fixedTime)
        {
            Jabbing = true;
            jabFinishSet = false;
            Mesh.SetActive(true);
            //GetComponent<AudioSource>().Play();
            attackedLast = Time.fixedTime;
        }

    }

    private void attackSecondary()
    {
        //Debug.Log("Fire");
        GameObject projectile = GameObject.Instantiate(Tridentin.gameObject, Tridentin.transform.position, Quaternion.identity);
        projectile.SetActive(true);
        projectile.transform.right = transform.right;
        projectile.transform.parent = null;
        projectile.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * transform.right;
    }

    public override void Attack(Vector2 PlayerDirection)
    {
        if (PlayerDirection.x < 0) AttackLeft();
        else if (PlayerDirection.x > 0) AttackRight();
        else if (PlayerDirection.y < 0) AttackDown();
        else if (PlayerDirection.y > 0) AttackUp();
    }
    public override void AttackSecondary(Vector2 PlayerDirection, bool PrimaryAttacking)
    {
        if (PrimaryAttacking) attackSecondary();
    }

    public void AttackRight()
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 0);
        transform.localPosition = new Vector3(0, JabYOffset, 0);
        JabVector = new Vector2(JabRightDistance, JabYOffset);
        attack();
    }

    public void AttackLeft()
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 180);
        transform.localPosition = new Vector3(0,  JabYOffset,0);
        JabVector = new Vector2(-JabLeftDistance, JabYOffset);

        attack();
    }

    public void AttackDown()
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 270);
        transform.localPosition = new Vector3(-JabXOffset, 0,  0);
        JabVector = new Vector2(-JabXOffset, -JabDownDistance);
        attack();
    }

    public void AttackUp()
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 90);
        transform.localPosition = new Vector3(JabXOffset, 0,  0);
        JabVector = new Vector2(JabXOffset, JabUpDistance);
        attack();
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
