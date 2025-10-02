using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class dialogueController : MonoBehaviour
{
    public GameObject char1;
    public GameObject char2;
    public GameObject textBox;
    public TMP_Text text;
    public TMP_Text nameText;
    public TMP_Text testText;
    public bool dialogueFlag = false;
    public int dialogueCount = 0;
    public bool dialogueFinished = false;
    private float textSpeed = 0.005f;
    public TextAsset dialogueText;
    public int currentDialogue;


    private Dialogue[] dialogueArray = new Dialogue[66];
    void Start()
    {
        LoadDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueFlag) 
        {
            startDialogue();
            dialogueFinished = false;
            dialogueFlag = false;
        }
        if (dialogueArray[dialogueCount].character == "Orpheus")
        {
            Debug.Log("hi");
            char1.transform.position = new Vector3(char1.transform.position.x, 150, char1.transform.position.z);
        }
        else 
        {
            char1.transform.position = new Vector3(char1.transform.position.x, 170, char1.transform.position.z);
        }
        Debug.Log(dialogueArray[dialogueCount].character.Trim().Equals("Orpheus", StringComparison.OrdinalIgnoreCase));
    }

    void startDialogue() 
    {
        char1.SetActive(true);
        char2.SetActive(true);
        textBox.SetActive(true);
        nameText.text = dialogueArray[dialogueCount].character;
        testText.text = dialogueArray[dialogueCount].portrait;
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText() 
    {
        text.text = "";
        for (int i = 0; i < dialogueArray[dialogueCount].text.Length + 1; i++) 
        {
            if (i < dialogueArray[dialogueCount].text.Length)
            {
                if (dialogueArray[dialogueCount].text.Substring(i, 1) == "/")
                {
                    text.text += "\n";
                }
                else
                {
                    text.text += dialogueArray[dialogueCount].text.Substring(i, 1);
                }
            }
            yield return new WaitForSeconds(textSpeed);
        }
        dialogueCount++;
        dialogueFinished = true;
    }

    void LoadDialogue() 
    {
        currentDialogue = 0;
        dialogueText = Resources.Load<TextAsset>("engDialogue");
        string[] dialogueStrings = dialogueText.text.Split("\n");
        for (int i = 0; i < dialogueStrings.Length; i++)
        {
            if (i % 2 == 0)
            {
                dialogueArray[currentDialogue] = new Dialogue();
                if (dialogueStrings[i].Contains("["))
                {
                    string[] portraitName = dialogueStrings[i].Split("[");
                    dialogueArray[currentDialogue].character = portraitName[0];
                    dialogueArray[currentDialogue].portrait = portraitName[1].Substring(0, portraitName[1].Length - 2);
                }
                else
                {
                    dialogueArray[currentDialogue].character = dialogueStrings[i];
                    dialogueArray[currentDialogue].portrait = dialogueArray[currentDialogue].character;
                }
            }
            else
            {
                dialogueArray[currentDialogue].text = dialogueStrings[i];
                currentDialogue++;
            }
        }
        char1.SetActive(false);
        char2.SetActive(false);
        textBox.SetActive(false);
    }
}

public class Dialogue
{
    public string text;
    public string character;
    public string portrait;
}
