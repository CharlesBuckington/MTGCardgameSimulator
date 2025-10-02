using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class ManaSymbol
{
    public string symbol;
    public int amount;
}

[Serializable]
public class CardDataModel
{
    public string id;
    public string name;
    public string rarity;
    public List<string> types;
    public List<ManaSymbol> manaCost;
    public string rulesText;
    public int power;
    public int toughness;
    public string set;
    public List<string> keywords;

    // Optional helper: convert list into a dictionary at runtime
    public Dictionary<string, int> GetManaCostDictionary()
    {
        Dictionary<string, int> dict = new();
        foreach (var m in manaCost)
        {
            if (!dict.ContainsKey(m.symbol)) dict[m.symbol] = 0;
            dict[m.symbol] += m.amount;
        }
        return dict;
    }
}

[Serializable]
public class CardDatabase
{
    public List<CardDataModel> cards;
}

public class CardParser : MonoBehaviour
{
    [SerializeField]
    CardObject cardObjectPrefab;
    public List<CardDataModel> cards = new();

    private void Awake() => LoadCards();

    private void Start()
    {
        SpawnCard(cards[1]);
    }

    private void LoadCards()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "cardData.json");
        if (!File.Exists(path))
        {
            Debug.LogError($"File not found at {path}");
            return;
        }

        string json = File.ReadAllText(path);
        CardDatabase db = JsonUtility.FromJson<CardDatabase>(json);
        if (db == null || db.cards == null)
        {
            Debug.LogError("Failed to parse JSON");
            return;
        }

        cards = db.cards;
        Debug.Log($"Loaded {cards.Count} cards.");
    }

    private void SpawnCard(CardDataModel data)
    {
        Instantiate(cardObjectPrefab).Initialize(data);
    }
}

