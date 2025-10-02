using System.Collections.Generic;
using UnityEngine;

public class ProgressionSystem : MonoBehaviour
{
    public static ProgressionSystem instance;
    public List<Card> acquiredCards = new List<Card>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCard(Card card)
    {
        acquiredCards.Add(card);
        Debug.Log("Card added: " + card.name);
    }
}