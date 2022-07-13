    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class CellStack : MonoBehaviour
    {
        public int roadsNum;
        public int blocksNum;
        public int emptyNum;

        private List<CellContent> stack = new List<CellContent>();
        public UnityEvent onUpdated;

        public int Left => stack.Count;

        private void Start()
        {
            for (int i = 0; i < roadsNum; i++)
                stack.Add(new CellRoad());
            for (int i = 0; i < blocksNum; i++)
                stack.Add(new CellBlocks());
            for (int i = 0; i < emptyNum; i++)
                stack.Add(new CellEmpty());
            onUpdated.Invoke();
        }

        public CellContent PickRandom()
        {
            if (Left > 0)
            {
                var rIdx = Random.Range(0, stack.Count);
                var cell = stack[rIdx];
                stack.Remove(cell);
                onUpdated.Invoke();
                return cell;
            }

            return null;
        }

        public void Add(CellContent cell)
        {
            stack.Add(cell);
            onUpdated.Invoke();
        }
    }