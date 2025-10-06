using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LamentationUIManager : MonoBehaviour
{
    [Header("Lamentation References")]

    [SerializeField] GameObject lamentationPanel;
    [SerializeField] TextMeshProUGUI lamentationTitle;
    [SerializeField] TextMeshProUGUI lamentationDescription;
    [SerializeField] Image lamentationImage;
    [SerializeField] Image activeLamentationImage;
    [SerializeField] Button lamentationButton;
    public DialogueQueues dialogue;


    // public properties

    public bool LamentationActivated => lamentationActivated;

    // Private variables

    LamentationSO activeLamentation;

    bool lamentationActivated = false;

    private void Awake()
    {
        
        
        
    }

    private void Start()
    {
        
    }

    public void SetActiveLamentation()
    {
        activeLamentation = LamentationSystem.instance.ActiveLamentation;
        Debug.Log(activeLamentation.Description);
        activeLamentationImage.enabled = false;
    }

    public void DisplayActiveLamentation()
    {
        lamentationPanel.SetActive(true);

        Debug.Log(activeLamentation);

        if (activeLamentation)
        {
            lamentationTitle.text = activeLamentation.Title;
            lamentationDescription.text = activeLamentation.Description;
            lamentationImage.sprite = activeLamentation.Image;
        }

        




    }

    public void StopDisplayingActiveLamentation()
    {
        lamentationPanel.SetActive(false);
        activeLamentationImage.enabled = true;

        if (activeLamentation)
        {
            activeLamentationImage.sprite = activeLamentation.Image;
        }

        
        lamentationActivated = true;
        dialogue.convo1();
    }
}
