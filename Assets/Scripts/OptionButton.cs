using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color unselectedColor;
    public Color selectedColor;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
