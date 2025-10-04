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
    public bool dialogueFlag = false;
    public int dialogueCount = 0;
    public bool dialogueFinished = false;
    private float textSpeed = 0.005f;
    public TextAsset dialogueText;
    public int currentDialogue;
    public int currentConversation;

    public Color char1norm;
    public Color char2norm;
    public Color char1dark;
    public Color char2dark;

    public UnityEngine.UI.Image p1image;
    public UnityEngine.UI.Image p2image;

    public npcPortraits npcPortraits;

    private Dialogue[] dialogueArray = new Dialogue[66];
    void Start()
    {
        LoadDialogue(0);
        dialogueFinished = true;
        p1image = char1.gameObject.GetComponent<UnityEngine.UI.Image>();
        p2image = char2.gameObject.GetComponent<UnityEngine.UI.Image>();
        Debug.Log(currentDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            if (dialogueFinished)
            {
                dialogueFlag = true;
            }
        }
        if (dialogueFlag && dialogueFinished)
        {
            startDialogue();
            if (dialogueCount != 0)
            {
                dialogueFinished = false;
            }
            Debug.Log("dialogue finished = false;");
            dialogueFlag = false;
        }
    }

    void startDialogue() 
    {
        char1.SetActive(true);
        char2.SetActive(true);
        textBox.SetActive(true);
        if (dialogueCount < currentDialogue)
        {
            nameText.text = dialogueArray[dialogueCount].character;
            int dialogueIndex = dialogueCount;
            StartCoroutine(AnimateText(dialogueIndex));
            dialogueCount++;
            if (dialogueCount > 0)
            {

                if (dialogueArray[dialogueCount - 1].character.Trim() == "Orpheus")
                {
                    npcPortraits.setPortrait("Char_Hermes_shaded");
                }
                else
                {
                    npcPortraits.setPortrait("Char_Hermes");
                }
            }
        }
        else 
        {
            Debug.Log("no more dialogue");
            dialogueCount = 0;
            dialogueFinished = true;
            Debug.Log("Dialogue finished = true"); 
            dialogueFlag = false;
            if (currentConversation < 13)
            {
                currentConversation++;
                LoadDialogue(currentConversation);
            }
        }
    }

    IEnumerator AnimateText(int dialogueNum) 
    {
        text.text = "";
        for (int i = 0; i < dialogueArray[dialogueNum].text.Length + 1; i++)
        {
            if (i < dialogueArray[dialogueNum].text.Length)
            {
                if (dialogueArray[dialogueNum].text.Substring(i, 1) == "/")
                {
                    text.text += "\n";
                }
                else
                {
                    text.text += dialogueArray[dialogueNum].text.Substring(i, 1);
                }
            }
            yield return new WaitForSeconds(textSpeed);
        }
        dialogueFinished = true;
    }

    void LoadDialogue(int dialogueNum) 
    {
        currentDialogue = 0;
        dialogueText = Resources.Load<TextAsset>("dialogue/dlg" + dialogueNum);
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
