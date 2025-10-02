using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ManaColor
{
    NULL,
    Generic,
    White,
    Blue,
    Black,
    Red,
    Green
}

public class ManaSymbolObject : MonoBehaviour
{
    [SerializeField]
    Image icon;
    [SerializeField]
    TextMeshProUGUI amountText;

    public void Initialize(ManaColor color, int amount = 1)
    {
        amountText.text = amount.ToString();
        amountText.enabled = amount == 1 ? false : true;
        switch (color)
        {
            case ManaColor.NULL:
                break;
            case ManaColor.Generic:
                icon.color = Color.gray;
                break;
            case ManaColor.White:
                icon.color = Color.white;
                break;
            case ManaColor.Blue:
                icon.color = Color.blue;
                break;
            case ManaColor.Black:
                icon.color = Color.black;
                break;
            case ManaColor.Red:
                icon.color = Color.red;
                break;
            case ManaColor.Green:
                icon.color = Color.green;
                break;
            default:
                break;
        }
    }
}
