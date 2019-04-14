using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
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
    private bool Jabbing = false;

    public GameObject Mesh;

    public enum AttackType
    {
        Jab,
        Slash
    }
    public AttackType Attack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if(Jabbing && Vector3.Magnitude(transform.localPosition) < JabDistance)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, JabVector, JabSpeed * Time.deltaTime);
        }else if (Jabbing)
        {
            Jabbing = false;
            JabFinish = Time.fixedTime;
        }else if(JabFinish + JabDuration < Time.fixedTime)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            Mesh.SetActive(false);
        }
	}

    private void attack()
    {
        //JabFinish = Time.fixedTime + 2;
        if(attackedLast + AttackWait < Time.fixedTime)
        {
            Jabbing = true;
            Mesh.SetActive(true);
            GetComponent<AudioSource>().Play();
            attackedLast = Time.fixedTime;
        }
        
    }



    public override void AttackRight()
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 0);
        transform.localPosition = new Vector3(0, JabYOffset, 0);
        JabVector = new Vector2(JabRightDistance, JabYOffset);
        attack();
    }

    public override void AttackLeft()
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 180);
        transform.localPosition = new Vector3(0, JabYOffset, 0);
        JabVector = new Vector2(-JabLeftDistance, JabYOffset);

        attack();
    }

    public override void AttackDown()
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 270);
        transform.localPosition = new Vector3(-JabXOffset, 0, 0);
        JabVector = new Vector2(-JabXOffset, -JabDownDistance);
        attack();
    }

    public override void AttackUp()
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 90);
        transform.localPosition = new Vector3(JabXOffset, 0, 0);
        JabVector = new Vector2(JabXOffset, JabUpDistance);
        attack();
    }
}
