using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color unselectedColor;
    public Color selectedColor;

    public void Selected(bool isSelected)
    {
        if (isSelected)
        {
            text.color = selectedColor;
        }
        else
        {
            text.color = unselectedColor;
        }
    }
}
