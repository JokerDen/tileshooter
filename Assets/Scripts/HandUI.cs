using System;
using System.Collections.Generic;
using UnityEngine;

public class HandUI : MonoBehaviour
{
    [SerializeField] CellItemUI itemPrefab;
    
    private List<CellItemUI> items = new List<CellItemUI>(); 

    void Start()
    {
        for (int i = 0; i < PanelkaGame.current.hand.size; i++)
        {
            var cell = PanelkaGame.current.hand.GetItem(i);
            if (cell != null)
                AddCell(cell);
        }
        
        PanelkaGame.current.hand.onAdded.AddListener(AddCell);
        PanelkaGame.current.hand.onRemoved.AddListener(RemoveCell);
    }

    private void RemoveCell(CellContent arg0)
    {
        foreach (var item in items)
        {
            if (item.data == arg0)
            {
                RemoveItem(item);
                return;
            }
        }
    }

    private void RemoveItem(CellItemUI item)
    {
        items.Remove(item);
        Destroy(item.gameObject);
    }

    private void AddCell(CellContent arg0)
    {
        var item = Instantiate(itemPrefab, transform);
        item.SetUp(arg0);
        items.Add(item);
    }

    private void UpdateHand()
    {
        
    }
}
