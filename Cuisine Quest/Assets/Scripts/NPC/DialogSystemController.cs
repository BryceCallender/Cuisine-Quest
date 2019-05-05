﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystemController : MonoBehaviour
{
    private Queue<string> messages;

    public Animator animator; 
    public TextMeshProUGUI characterDialogText;
    public GameObject dialogPopup;
    public CiscoTesting player;
    public bool paused;
    public bool isTyping;

    private Coroutine coroutine;
    public float skipTimer;
    private float timeTillSkip = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        messages = new Queue<string>();
        dialogPopup.SetActive(false);
    }

    private void Update()
    {
        if(isTyping)
        {
            skipTimer += Time.deltaTime;
        }
    }

    public void StartDialog(Dialog dialog)
    {
        messages.Clear();
        StopPlayerMovement();
        characterDialogText.text = string.Empty;

        foreach (string message in dialog.messages)
        {
            messages.Enqueue(message);
        }

        dialogPopup.SetActive(true);
        animator.SetBool("isOpen", true);

        DisplayMessage();
    }

    public void DisplayMessage()
    {
        if (messages.Count == 0)
        {
            Debug.Log("No Messages");
            animator.SetBool("isOpen", false);
            StartPlayerMovement();
            isTyping = false;
            float time = animator.GetCurrentAnimatorStateInfo(0).length;
            //Turns off the dialog box after the animation length is done
            //So the engine doesnt have to worry about rendering. 
            Invoke("TurnOffDialogBox", time);
            return;
        }

        Debug.Log(messages.Count);
        string sentence = messages.Dequeue();
        Debug.Log(sentence);
        StopAllCoroutines();

        //Slowly displays the message that it should be showing
        dialogPopup.SetActive(true);
        animator.SetBool("isOpen", true);
        characterDialogText.text = string.Empty;
        coroutine = StartCoroutine(SlowlyDisplayMessage(sentence));
    }

    private IEnumerator SlowlyDisplayMessage(string message)
    {
        characterDialogText.text = string.Empty;
        foreach (char letter in message)
        {
            characterDialogText.text += letter;
            yield return null;
        }
    }

    public bool isEmpty()
    {
        return messages.Count == 0 && !dialogPopup.activeSelf;
    }

    public void TurnOffDialogBox()
    {
        dialogPopup.SetActive(false);
    }

    private void StopPlayerMovement()
    {
        Debug.Log("Stop player movement");
        player.GetComponent<PlayerController>().playerCanMove = false;
    }

    private void StartPlayerMovement()
    {
        Debug.Log("Resume player movement");
        player.GetComponent<PlayerController>().playerCanMove = true;
    }
}