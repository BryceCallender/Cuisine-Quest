using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon {
    public int KnifeCount = 1;

    public float KnifeThrowingSpeed = 20f;

    public float AttackWait = 0.5f;
    private float attackedLast = float.NegativeInfinity;
    public GameObject ThrowingKnife;

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Jabbing && Vector3.Magnitude(transform.localPosition) < Vector3.Magnitude(JabVector))
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, JabVector, JabSpeed * Time.deltaTime);
        }
        else if (Jabbing)
        {
            Jabbing = false;
            JabFinish = Time.fixedTime;
        }
        else if (JabFinish + JabDuration < Time.fixedTime)
        {
            AttackAbort();
        }
    }

    private void attack()
    {
        //JabFinish = Time.fixedTime + 2;
        if (attackedLast + AttackWait < Time.fixedTime && KnifeCount > 0)
        {
            Jabbing = true;
            Mesh.SetActive(true);
            //GetComponent<AudioSource>().Play();
            attackedLast = Time.fixedTime;
        }

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
        if(KnifeCount > 0)
        {
            AttackAbortForced();
            GameObject throwingKnife = Instantiate(ThrowingKnife, gameObject.transform.position, Quaternion.identity);
            throwingKnife.GetComponent<Rigidbody2D>().velocity = KnifeThrowingSpeed * PlayerDirection;
            throwingKnife.transform.right = PlayerDirection;

            KnifeCount -= 1;
        }
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
        transform.localPosition = new Vector3(0, JabYOffset, 0);
        JabVector = new Vector2(-JabLeftDistance, JabYOffset);

        attack();
    }

    public void AttackDown()
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 270);
        transform.localPosition = new Vector3(-JabXOffset, 0, 0);
        JabVector = new Vector2(-JabXOffset, -JabDownDistance);
        attack();
    }

    public void AttackUp()
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 90);
        transform.localPosition = new Vector3(JabXOffset, 0, 0);
        JabVector = new Vector2(JabXOffset, JabUpDistance);
        attack();
    }
    public void attackEnd()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        Mesh.SetActive(false);
        Jabbing = false;
    }

    public override bool AttackAbort()
    {
        attackEnd();

        return true;
    }
    public override void AttackAbortForced()
    {
        attackEnd();
    }
}
