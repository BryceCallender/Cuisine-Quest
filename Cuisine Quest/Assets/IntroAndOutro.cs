using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class IntroAndOutro : MonoBehaviour 
{
    public GameObject ui;
    public TextMeshProUGUI screenText;
    [TextArea]
    public string intro;
    [TextArea]
    public string outro;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if(!File.Exists(Path.Combine(Application.persistentDataPath,"PlayerItems.json")))
        {
            StartIntro();
        }
        else
        {
            ui.SetActive(false);
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartOutro();
        }
    }

    public void StartIntro()
    {
        PauseMenu.paused = true;
        StartCoroutine(printText(true, intro));

    }

    public void StartOutro()
    {
        PauseMenu.paused = true;
        animator.SetTrigger("EndGame");
        StartCoroutine(printText(false, outro));
    }

    IEnumerator printText(bool intro, string text)
    {
        screenText.text = string.Empty;

        foreach (char letter in text)
        {
            screenText.text += letter;
            //Does 1 character on screen every frame
            yield return null;
        }

        if(intro)
        {
            animator.SetTrigger("BeginGame");
            screenText.text = string.Empty;
            PauseMenu.paused = false;
        }
        else
        {

        }

    }
}
