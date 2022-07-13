    using UnityEngine;

    public class CellContentProvider : MonoBehaviour
    {
        private CellContent content;
        public CellContent awaiting;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ForceContent(new CellBlocks());
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
                ForceContent(new CellRoad());
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
                ForceContent(new CellEmpty());
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
                ForceContent(null);
        }

        private void ForceContent(CellContent type)
        {
            content = type;
            Next();
        }

        public void Next()
        {
            PanelkaGame.current.hand.Remove(awaiting);
            
            awaiting = null;
            
            if (content is CellBlocks)
                awaiting = new CellBlocks();
            
            if (content is CellRoad)
                awaiting = new CellRoad();
            
            if (content is CellEmpty)
                awaiting = new CellEmpty();
            
            var input = FindObjectOfType<PointerInput>();
            input.ResetCast();
        }
    }