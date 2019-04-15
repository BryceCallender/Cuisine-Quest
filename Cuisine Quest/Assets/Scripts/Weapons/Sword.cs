using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
    //public float AttackWait = 0.5f;
    private float attackedLast = float.NegativeInfinity;

    private Vector2 JabVector;

    private float JabFinish;
    private bool Jabbing = false;

    private bool Slashing = false;

    public JabbingProperties JP;
    public SlashingProperties SP;

    public GameObject Mesh;

    public enum AttackType
    {
        Jab,
        Slash
    }
    public AttackType MyAttack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (MyAttack == AttackType.Jab) JabAttack(JP, ref JabVector, ref JabFinish, ref Jabbing);
        else if (MyAttack == AttackType.Slash)
        {
            
            SlashAttack(SP, ref Slashing);
        }
	}

    private void attack()
    {
        //JabFinish = Time.fixedTime + 2;
        if(attackedLast + JP.AttackWait < Time.fixedTime)
        {
            Jabbing = true;
            Mesh.SetActive(true);
            GetComponent<AudioSource>().Play();
            attackedLast = Time.fixedTime;
        }
        
    }

    public override void Attack(Vector2 PlayerDirection)
    {
        if(MyAttack == AttackType.Jab)
        {
            if (PlayerDirection.x < 0) AttackLeft();
            else if (PlayerDirection.x > 0) AttackRight();
            else if (PlayerDirection.y < 0) AttackDown();
            else if (PlayerDirection.y > 0) AttackUp();

        }

        else if(MyAttack == AttackType.Slash)
        {
            transform.localPosition = SP.HiltLocation;
            transform.localEulerAngles = new Vector3(0, 0, SP.RotationBegin);

            //transform.right = PlayerDirection;

            if (PlayerDirection.x < 0)
            {
                transform.localPosition = SP.HiltLeftLocation;
                SP.myDirection = SlashingProperties.Direction.Left;
            }
            else if (PlayerDirection.x > 0)
            {
                transform.localPosition = SP.HiltLocation;
                SP.myDirection = SlashingProperties.Direction.Right;
            }
            else if (PlayerDirection.y > 0)
            {
                transform.localPosition = SP.HiltUpLocation;
                SP.myDirection = SlashingProperties.Direction.Up;
            }
            else if (PlayerDirection.y < 0)
            {
                transform.localPosition = SP.HiltDownLocation;
                SP.myDirection = SlashingProperties.Direction.Down;
            }

                Slashing = true;
            Mesh.SetActive(true);
            //SlashAttack(SP);
        }
    }
    public override void AttackSecondary(Vector2 PlayerDirection, bool PrimaryAttacking)
    {
        Debug.Log("I ain't throw'n me blade!");
    }

    public void AttackRight()
    {
        if (attackedLast + JP.AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 0);
        transform.localPosition = new Vector3(0, JP.JabYOffset, 0);
        JabVector = new Vector2(JP.JabRightDistance, JP.JabYOffset);
        attack();
    }

    public void AttackLeft()
    {
        if (attackedLast + JP.AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 180);
        transform.localPosition = new Vector3(0, JP.JabYOffset, 0);
        JabVector = new Vector2(-JP.JabLeftDistance, JP.JabYOffset);

        attack();
    }

    public void AttackDown()
    {
        if (attackedLast + JP.AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 270);
        transform.localPosition = new Vector3(-JP.JabXOffset, 0, 0);
        JabVector = new Vector2(-JP.JabXOffset, -JP.JabDownDistance);
        attack();
    }

    public void AttackUp()
    {
        if (attackedLast + JP.AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 90);
        transform.localPosition = new Vector3(JP.JabXOffset, 0, 0);
        JabVector = new Vector2(JP.JabXOffset, JP.JabUpDistance);
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
