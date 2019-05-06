using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCredits : MonoBehaviour 
{
    public GameObject quitButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OOF");
        collision.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(true);
    }
}
