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
    }
    public void convo2() 
    {
        controller.dialogueFlag = true;
    }
}
