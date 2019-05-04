﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : NPC
{

    public Item key;
    BoxCollider2D gate;
    Animator anim;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        gate = GetComponent<BoxCollider2D>();
        dialogSystemController = FindObjectOfType<DialogSystemController>();
        characterDialog = gameObject.GetComponents<CharacterDialog>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<CiscoTesting>().items == null)
            {
                characterDialog[0].EnableDialog();
            }
            else if (collision.gameObject.GetComponent<CiscoTesting>().items.ContainsKey(key))
            {
                characterDialog[1].EnableDialog();
                anim.SetBool("Unlocked", true);
                gate.enabled = false;
            }
            else
            {
                characterDialog[0].EnableDialog();
            }
        }
        
    }
}