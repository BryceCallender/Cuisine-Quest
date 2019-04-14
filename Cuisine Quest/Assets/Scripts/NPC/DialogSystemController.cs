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

        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            Debug.Log("Hit space");
            DisplayMessage();
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("DialogBox_Close"))
        {
            print("Closing");
        }
    }

    public void StartDialog(Dialog dialog)
    {
        messages.Clear();

        foreach (string message in dialog.messages)
        {
            messages.Enqueue(message);
        }

        DisplayMessage();
    }

    private void DisplayMessage()
    {
        if (isEmpty())
        {
            Debug.Log("Empty");
            animator.SetBool("isOpen", false);
            float time = animator.GetCurrentAnimatorStateInfo(0).length;
            //Turns off the dialog box after the animation length is done
            //So the engine doesnt have to worry about rendering. 
            Invoke("TurnOffDialogBox", time);
            InteractionUnPauseMovementAndCamera();
            return;
        }

        Debug.Log(messages.Count);
        string sentence = messages.Dequeue();
        Debug.Log(sentence);

        //Slowly displays the message that it should be showing
        dialogPopup.SetActive(true);
        animator.SetBool("isOpen", true);
        coroutine = StartCoroutine(SlowlyDisplayMessage(sentence));
    }

    private IEnumerator SlowlyDisplayMessage(string message)
    {
        characterDialogText.text = "";
        isTyping = true;
        foreach (char letter in message)
        {
            characterDialogText.text += letter;
            //If the user hits space and the code is typing 
            //stop it. Set the text to empty, stop the coroutine 
            //and set the text directly to the message text.
            if(Input.GetKeyDown(KeyCode.Space) && isTyping && skipTimer > timeTillSkip)
            {
                characterDialogText.text = "";
                StopCoroutine(coroutine);
                characterDialogText.text = message;
                isTyping = false;
                skipTimer = 0.0f;
            }
            //Does 1 character on screen every frame
            yield return null;
        }
        isTyping = false;
    }

    public bool isEmpty()
    {
        return messages.Count == 0;
    }

    public void TurnOffDialogBox()
    {
        dialogPopup.SetActive(false);
    }

    private void InteractionPauseMovementAndCamera()
    {
        //this.GetComponent<PlayerMovement>().enabled = false;
        //this.GetComponent<CameraController>().enabled = false;
    }

    private void InteractionUnPauseMovementAndCamera()
    {
        //this.GetComponent<PlayerMovement>().enabled = true;
        //this.GetComponent<CameraController>().enabled = true;
    }
}