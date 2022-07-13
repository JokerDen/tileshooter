using DefaultNamespace;
using UnityEngine;

public class PanelkaGame : MonoBehaviour
{
    public static PanelkaGame current;

    private void Awake()
    {
        current = this;
    }

    public CellStack stack;
    public CellHand hand;
    public GameField gameField;
    public GameUI ui;

    public int autoNextNum;

    public void Next()
    {
        for (int i = 0; i < autoNextNum; i++)
        {
            var cell = stack.PickRandom();
            if (cell != null && !gameField.PlaceRandom(cell))
                stack.Add(cell);
        }

        var num = hand.GetEmptySize();
        for (int i = 0; i < num; i++)
        {
            var cell = stack.PickRandom();
            if (cell != null)
                hand.Add(cell);
        }
    }
}
