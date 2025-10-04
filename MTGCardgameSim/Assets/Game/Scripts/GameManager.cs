using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CardParser parser;
    [SerializeField] CardObject cardObjectPrefab;

    private Deck deck;
    private const int DeckSize = 40;

    [SerializeField] CardZone handZone;

    private void Start()
    {
        GenerateDeck();
    }

    void GenerateDeck()
    {
        deck = new Deck();
        for (int i = 0; i < DeckSize; i++)
        {
            deck.AddToTop(parser.GetRandomCard());
        }
    }

    void GenerateDeck(List<CardDataModel> _deck)
    {
        deck = new Deck();
        for (int i = 0; i < DeckSize; i++)
        {
            deck.AddToTop(_deck[i]);
        }
    }

    public void DrawCard()
    {
        handZone.AddItem(SpawnCardObject(deck.Draw()));
    }

    public CardObject SpawnCardObject(CardDataModel data)
    {
        CardObject obj = Instantiate(cardObjectPrefab);
        obj.Initialize(data);
        return obj;
    }
}
