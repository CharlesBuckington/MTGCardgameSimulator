using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Deck
{
    private List<CardDataModel> cards = new List<CardDataModel>();

    public int Count => cards.Count;

    public void AddToTop(CardDataModel card)
    {
        cards.Insert(0, card);
    }

    public void AddToBottom(CardDataModel card)
    {
        cards.Add(card);
    }

    public CardDataModel Draw()
    {
        if (cards.Count == 0)
        {
            Debug.LogWarning("Deck is empty!");
            return null;
        }

        CardDataModel top = cards[0];
        cards.RemoveAt(0);
        return top;
    }

    public CardDataModel PeekTop()
    {
        return cards.Count > 0 ? cards[0] : null;
    }

    public CardDataModel PeekBottom()
    {
        return cards.Count > 0 ? cards[cards.Count - 1] : null;
    }

    public List<CardDataModel> GetTop(int n)
    {
        n = Mathf.Min(n, cards.Count);
        return cards.GetRange(0, n);
    }

    public void Shuffle()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int rnd = Random.Range(i, cards.Count);
            (cards[i], cards[rnd]) = (cards[rnd], cards[i]);
        }
    }

    public void ReorderTop(int n, List<CardDataModel> newOrder)
    {
        n = Mathf.Min(n, cards.Count);
        for (int i = 0; i < n; i++)
        {
            cards[i] = newOrder[i];
        }
    }

    public void PrintDeck()
    {
        Debug.Log("Deck:");
        foreach (var card in cards)
            Debug.Log(card.name);
    }
}