    using UnityEngine;

    public class DayTimeManager : MonoBehaviour
{
    public StateData dayTime;

    [Multiline]
    public string[] labels;

    private int current = -1;

    public DayTimeUI ui;

    private void Start()
    {
        dayTime.onChanged.AddListener(ShowText);
    }

    private void ShowText()
    {
        var idx = dayTime.StateIndex;
        if (idx < labels.Length && idx > current)
        {
            current = idx;
            var label = labels[current];
            ui.Show(label);
        }
    }
}
