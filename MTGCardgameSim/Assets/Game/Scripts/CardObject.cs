using TMPro;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI cardNameText;
    [SerializeField]
    TextMeshProUGUI cardTypesText;
    [SerializeField]
    TextMeshProUGUI cardRulesText;

    [SerializeField]
    GameObject powerToughnessBox;
    [SerializeField]
    TextMeshProUGUI powerToughnessText;

    [SerializeField]
    Transform manaSymbolParent;
    [SerializeField]
    ManaSymbolObject manaSymbolPrefab;

    CardDataModel data;
    
    public void Initialize(CardDataModel _data)
    {
        data = _data;
        cardNameText.text = data.name;

        string types = data.types[0];
        for (int i = 1; i < data.types.Count; i++)
        {
            types += " " + data.types[i];
        }
        cardTypesText.text = types;
        cardRulesText.text = data.rulesText;
        powerToughnessText.text = data.power + " / " + data.toughness;
        powerToughnessBox.SetActive((data.power == -1 && data.toughness == -1) ? false : true);

        foreach (Transform child in manaSymbolParent)
        {
            Destroy(child.gameObject);
        }
        foreach (ManaSymbol mana in data.manaCost)
        {
            ManaSymbolObject obj = Instantiate(manaSymbolPrefab, manaSymbolParent);
            ManaColor color = ConvertSymbolToColor(mana.symbol);
            obj.Initialize(color, mana.amount);
        }
    }

    private ManaColor ConvertSymbolToColor(string symbol)
    {
        switch (symbol.ToUpper())
        {
            case "W": return ManaColor.White;
            case "U": return ManaColor.Blue;
            case "B": return ManaColor.Black;
            case "R": return ManaColor.Red;
            case "G": return ManaColor.Green;
            default: return ManaColor.Generic;
        }
    }
}
