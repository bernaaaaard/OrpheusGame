using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LamentationUIManager : MonoBehaviour
{
    [Header("Lamentation References")]

    [SerializeField] GameObject lamentationPanel;
    [SerializeField] TMP_Text lamentationTitle;
    [SerializeField] TMP_Text lamentationDescription;
    [SerializeField] Image lamentationImage;
    [SerializeField] Button lamentationButton;


    // public properties

    public bool LamentationActivated => lamentationActivated;

    // Private variables

    LamentationSO activeLamentation;

    bool lamentationActivated = false;

    private void Start()
    {
        LamentationSystem.instance.SelectRandomLamentation();
    }

    public void DisplayActiveLamentation()
    {
        lamentationTitle.text = LamentationSystem.instance.ActiveLamentation.Title;
        lamentationDescription.text = LamentationSystem.instance.ActiveLamentation.Description;
        lamentationImage = LamentationSystem.instance.ActiveLamentation.Image;

        lamentationPanel.SetActive(true);

        
    }

    public void StopDisplayingActiveLamentation()
    {
        lamentationPanel.SetActive(false);

        lamentationActivated = true;

    }
}
