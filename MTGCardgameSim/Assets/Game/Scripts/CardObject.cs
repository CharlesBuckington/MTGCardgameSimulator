using TMPro;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cardNameText;
    [SerializeField] TextMeshProUGUI cardTypesText;
    [SerializeField] TextMeshProUGUI cardRulesText;

    [SerializeField] GameObject powerToughnessBox;
    [SerializeField] TextMeshProUGUI powerToughnessText;

    [SerializeField] Transform manaSymbolParent;
    [SerializeField] ManaSymbolObject manaSymbolPrefab;

    public CardZone CurrentZone { get; set; }

    private CardZone _lastZone;
    private Vector3 _lastLocalPos;

    private bool _dragging;
    private Vector3 _grabOffsetWorld; // keeps the grab point consistent

    CardDataModel data;
    public CardDataModel Data => data;

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) _dragging = false;
    }

    void OnDisable()
    {
        _dragging = false;
    }

    public void Initialize(CardDataModel _data)
    {
        data = _data;
        cardNameText.text = data.name;

        string types = data.types[0];
        for (int i = 1; i < data.types.Count; i++)
            types += " " + data.types[i];
        cardTypesText.text = types;

        cardRulesText.text = data.rulesText;
        powerToughnessText.text = data.power + " / " + data.toughness;
        powerToughnessBox.SetActive(!(data.power == -1 && data.toughness == -1));

        foreach (Transform child in manaSymbolParent)
            Destroy(child.gameObject);

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
