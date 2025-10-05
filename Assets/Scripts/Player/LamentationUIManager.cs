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


    // public properties

    public bool LamentationActivated => lamentationActivated;

    // Private variables

    LamentationSO activeLamentation;

    bool lamentationActivated = false;

    private void Awake()
    {
        LamentationSystem.instance.SelectRandomLamentation();
        activeLamentation = LamentationSystem.instance.ActiveLamentation;
        Debug.Log(activeLamentation.Description);
    }

    private void Start()
    {
        activeLamentationImage.enabled = false;
    }

    public void DisplayActiveLamentation()
    {
        lamentationPanel.SetActive(true);

        lamentationTitle.text = activeLamentation.Title;
        lamentationDescription.text = activeLamentation.Description;
        lamentationImage.sprite = activeLamentation.Image;




    }

    public void StopDisplayingActiveLamentation()
    {
        lamentationPanel.SetActive(false);
        activeLamentationImage.enabled = true;
        activeLamentationImage.sprite = activeLamentation.Image;
        lamentationActivated = true;

    }
}
