using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungenessClaws : HealthSystem {
    public DungenessHealth DH;

    public override void takeDamage(int damage)
    {
        DH.GetComponent<Dungeness>().ClawHit(damage);
    }

    

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            collision.gameObject.GetComponent<HealthSystem>().takeDamage(DH.gameObject.GetComponent<Dungeness>().ClawAttackPower);
        }
    }

}
