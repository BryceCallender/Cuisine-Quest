using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public int WeaponPower = 1;

    //public abstract void AttackRight();
    //public abstract void AttackLeft();
    //public abstract void AttackDown();
    //public abstract void AttackUp();

    public abstract void Attack(Vector2 PlayerDirection);
    public abstract void AttackSecondary(Vector2 PlayerDirection, bool PrimaryAttacking);

    public abstract bool AttackAbort();
    public abstract void AttackAbortForced();

    //private Vector2 JabVector;

    //private float JabFinish;
    //private bool Jabbing = false;

    protected void JabAttack(JabbingProperties jp, ref Vector2 JabVector, ref float JabFinish, ref bool Jabbing)
    {
        if (Jabbing && Vector3.Magnitude(transform.localPosition) < Vector3.Magnitude(JabVector))
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, JabVector, jp.Speed * Time.deltaTime);
        }
        else if (Jabbing)
        {
            Jabbing = false;
            JabFinish = Time.fixedTime;
        }
        else if (JabFinish + jp.JabDuration < Time.fixedTime)
        {
            AttackAbort();
        }
    }

    //private bool attacking = false;
    private bool rotating = false;
    private float slashWaiting = float.NegativeInfinity;
    private float targetAngle = 0;
    private float currentDisplacement = 0;
    private bool startRotating = true;
    protected void SlashAttack(SlashingProperties sp, ref bool attacking) //, Vector2 PlayerDirection)
    {

        
        float rotationOffset = 0;
        if (sp.myDirection == SlashingProperties.Direction.Left) rotationOffset = 180;
        else if (sp.myDirection == SlashingProperties.Direction.Up) rotationOffset = 90;
        else if (sp.myDirection == SlashingProperties.Direction.Down) rotationOffset = 270;

        if (attacking && startRotating)
        {
            transform.localEulerAngles = new Vector3(0, 0, sp.RotationBegin + rotationOffset);
            targetAngle = sp.RotationLength; // - sp.RotationBegin;// + rotationOffset;
            currentDisplacement = 0;
            startRotating = false;
        }

        

        if (attacking)
        {

            float zRot = transform.localEulerAngles.z;
            zRot = sp.RotationSpeed * Time.deltaTime;
            
            Debug.Log(transform.localEulerAngles.z);
            //if (zRot - sp.RotationBegin <  sp.RotationLength + rotationOffset || zRot  > 180 + rotationOffset)
            if(currentDisplacement < targetAngle)
            {

                transform.Rotate(new Vector3(0, 0, sp.RotationSpeed * Time.deltaTime));
                currentDisplacement += zRot;
                //Debug.Log("rotating");
                rotating = true;
            }
            else if (attacking && rotating)
            {
                slashWaiting = Time.fixedTime;
                rotating = false;
            }
            else if(slashWaiting + sp.WaitOnFinish < Time.fixedTime)
           
            {
                AttackAbort();
                attacking = false;
                startRotating = true;
            }
        }
        
    }
}
