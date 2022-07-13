using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class CellHand : MonoBehaviour
    {
        public int size;

        private List<CellContent> current = new List<CellContent>();

        public CellContentEvent onAdded = new CellContentEvent();
        public CellContentEvent onRemoved = new CellContentEvent();

        public int GetEmptySize()
        {
            return size - current.Count;
        }

        public void Add(CellContent cell)
        {
            current.Add(cell);
            onAdded.Invoke(cell);
        }

        public CellContent GetItem(int index)
        {
            if (index > 0 && index < current.Count)
                return current[index];
            return null;
        }

        public void Select(CellContent data)
        {
            PanelkaGame.current.gameField.provider.awaiting = PanelkaGame.current.gameField.provider.awaiting == data ? null : data;
            
            var input = FindObjectOfType<PointerInput>();
            input.ResetCast();
        }

        public void Remove(CellContent cell)
        {
            if (current.Contains(cell))
            {
                current.Remove(cell);
                onRemoved.Invoke(cell);
            }
        }
    }
}