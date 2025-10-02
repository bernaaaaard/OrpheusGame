using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AugmentUIManager : MonoBehaviour
{
   
    private Button[] buttons;
    private TextMeshProUGUI[] cardNames;
    private TextMeshProUGUI[] cardDescriptions;
    private GameObject currentPanel;
   // private List<string> collectedNames = new List<string>();

    //public Transform collectedListParent;  
    //public TextMeshProUGUI collectedTextPrefab;

    [Header("Available Cards (Assign in Inspector)")]
    private List<Card> augments;

    void Awake()
    {

        Card[] loadedCards = Resources.LoadAll<Card>("Cards");
        augments = new List<Card>(loadedCards);

        if (augments.Count == 0)
        {
            Debug.LogError("No Cards found");
        }

        currentPanel = gameObject;


        buttons = GetComponentsInChildren<Button>(true);


        cardNames = new TextMeshProUGUI[buttons.Length];
        cardDescriptions = new TextMeshProUGUI[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {

            cardNames[i] = buttons[i].transform.Find("name").GetComponent<TextMeshProUGUI>();
            cardDescriptions[i] = buttons[i].transform.Find("description").GetComponent<TextMeshProUGUI>();
        }


        currentPanel.SetActive(false);
    }

    public void ShowCards()
    {
        currentPanel.SetActive(true);
        DisablePlayerMovement();


        List<Card> cards = GetRandomCards(3);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < cards.Count)
            {
                Card card = cards[i];


                cardNames[i].text = card.name;
                cardDescriptions[i].text = card.description;


                int index = i;
                buttons[i].onClick.RemoveAllListeners();
                buttons[i].onClick.AddListener(() => OnCardSelected(cards[index]));
            }
        }
    }

    public void OnCardSelected(Card card)
    {
        //PlayerStats.instance.ApplyCardEffects(card);
        if (ProgressionSystem.instance != null)
        {
            ProgressionSystem.instance.AddCard(card);
        }
        else
        {
            Debug.Log("ProgressionSystem is null: " + card.name);
        }

        if (currentPanel != null)
        {

            currentPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("currentPanel is null!");
        }


        UpdateCollectedListUI(card);
        EnablePlayerMovement();
    }

    private List<Card> GetRandomCards(int count)
    {
        List<Card> availableCards = new List<Card>(augments);
        List<Card> chosenCards = new List<Card>();

        for (int i = 0; i < count && availableCards.Count > 0; i++)
        {
            int randIndex = Random.Range(0, availableCards.Count);
            chosenCards.Add(availableCards[randIndex]);
            availableCards.RemoveAt(randIndex);
        }

        return chosenCards;
    }


    private void DisablePlayerMovement()
    {
        // GameObject player = GameObject.FindGameObjectWithTag("Player");
        // if (player == null) return;

        // PlayerMovement movement = player.GetComponent<PlayerMovement>();
        // if (movement != null)
        // {
        //     movement.enabled = false;
        // }
        // else
        // {
        //     Debug.LogWarning("PlayerMovement component not found.");
        // }
    }


    private void EnablePlayerMovement()
    {
        // GameObject player = GameObject.FindGameObjectWithTag("Player");
        // if (player == null) return;

        // PlayerMovement movement = player.GetComponent<PlayerMovement>();
        // if (movement != null)
        // {
        //     movement.enabled = true;
        // }
        // else
        // {
        //     Debug.LogWarning("PlayerMovement component not found.");
        // }
    }



    public void UpdateCollectedListUI(Card card)
    {
        // collectedNames.Add(card.name);


        // TextMeshProUGUI newEntry = Instantiate(collectedTextPrefab, collectedListParent);
        // newEntry.text = card.name;
    }

}
