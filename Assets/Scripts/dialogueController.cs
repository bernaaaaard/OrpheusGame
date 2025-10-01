using System.Collections;
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
    private float textSpeed = 0.02f;

    private Dialogue[] dialogueArray = new Dialogue[2];
    void Start()
    {
        dialogueArray[0] = new Dialogue();
        dialogueArray[0].text = "This is a test of the dialogue system. I am testing if the text loads sequentially, and the text box appears.";
        dialogueArray[0].character = "Orpheus";
        dialogueArray[1] = new Dialogue();
        dialogueArray[1].text = "This is the second dialogue call";
        dialogueArray[1].character = "Eurydice";
        char1.SetActive(false);
        char2.SetActive(false);
        textBox.SetActive(false);
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
    }

    void startDialogue() 
    {
        char1.SetActive(true);
        char2.SetActive(true);
        textBox.SetActive(true);
        nameText.text = dialogueArray[dialogueCount].character;
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText() 
    {
        for (int i = 0; i < dialogueArray[dialogueCount].text.Length + 1; i++) 
        {
            text.text = dialogueArray[dialogueCount].text.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
        dialogueCount++;
        dialogueFinished = true;
    }
}

public class Dialogue
{
    public string text;
    public string character;
}
