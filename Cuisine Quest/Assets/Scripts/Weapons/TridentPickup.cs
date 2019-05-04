using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentPickup : MonoBehaviour {

    public Weapon PrefabTrident;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Weapon newWeapon = Instantiate(PrefabTrident, collision.transform.position, Quaternion.identity);
            newWeapon.name = "Trident";
            collision.GetComponent<CiscoTesting>().AddWeapon(newWeapon);
            Destroy(gameObject);
        }
    }
}
