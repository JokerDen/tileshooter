using UnityEngine;
using UnityEngine.UI;

public class ButtonNext : MonoBehaviour
{
    public TextFormat stackLeft;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Next);
        
        UpdateStackLeft();
        PanelkaGame.current.stack.onUpdated.AddListener(UpdateStackLeft);
;    }

    private void UpdateStackLeft()
    {
        stackLeft.SetText(PanelkaGame.current.stack.Left);
    }

    private void Next()
    {
        PanelkaGame.current.Next();
    }
}
