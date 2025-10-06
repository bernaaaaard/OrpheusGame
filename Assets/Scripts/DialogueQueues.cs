using System.Collections;
using UnityEngine;

public class DialogueQueues : MonoBehaviour
{
    public dialogueController controller;
    void Start()
    {

    }

    private void Update()
    {

    }

    public void convo1()
    {
        controller.dialogueFlag = true;
        StartCoroutine(dialogueThing());
    }
    public void convo2() 
    {
        controller.dialogueFlag = true;
    }

    IEnumerator dialogueThing() 
    {
        if (controller.dialogueFlag)
        {
            yield return null;
        }
        else 
        {
            controller.currentConversation = 3.5f;
            controller.dialogueFlag = true;
        }
    }
}
