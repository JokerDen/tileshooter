using System;
using UnityEngine;
using UnityEngine.UI;

public class CellItemUI : MonoBehaviour
{
    public Button button;
    public TextFormat label;

    public CellContent data;

    private void Start()
    {
        button.onClick.AddListener(Select);
    }

    private void Select()
    {
        PanelkaGame.current.hand.Select(data);
    }

    public void SetUp(CellContent arg0)
    {
        data = arg0;
        
        label.SetText(arg0);
    }
}
