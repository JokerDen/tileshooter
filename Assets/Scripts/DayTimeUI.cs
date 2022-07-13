using UnityEngine;
using UnityEngine.UI;

public class DayTimeUI : MonoBehaviour
{
    public Text label;

    public CustomInput input;

    public void Show(string text)
    {
        label.text = text;
        input.locked = true;
        gameObject.SetActive(true);
        
        Invoke("Hide", 3f);
    }

    private void Hide()
    {
        input.locked = false;
        gameObject.SetActive(false);
    }

    public void ShowFinish()
    {
        Show("you falling asleep...\nand this is the end of playtest demo 1\nthanks for playing!");
        CancelInvoke("Hide");
    }
}
