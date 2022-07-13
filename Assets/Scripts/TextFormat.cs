using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextFormat : MonoBehaviour
{
    private Text text;
    private string format;

    public void SetText(params object[] values)
    {
        if (text == null)
        {
            text = GetComponent<Text>();
            format = text.text;
        }

        text.text = string.Format(format, values);
    }
}
