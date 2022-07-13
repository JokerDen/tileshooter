using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public UILayer layer;
    private RectTransform thisRT;
    private Interactible interactible;
    
    private void Start()
    {
        if (interactible != null)
            UpdatePosition();
        else
            Remove();
    }

    public bool IsInteractible(Interactible interactible)
    {
        // return this.interactible == interactible;
        return this.interactible == interactible && (interactible == null || this.interactible.HasInteraction());
    }

    public void Remove()
    {
        interactible = null;
        Hide();
    }

    public void Show(Interactible arg0)
    {
        interactible = arg0;
        Show();
        UpdatePosition();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (interactible == null) return;

        if (thisRT == null)
            thisRT = GetComponent<RectTransform>();
        
        var screenPoint = RectTransformUtility.WorldToScreenPoint(layer.cam, interactible.target.position);
        Vector2 localPoint;
        // cam null for overlay canvas
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(layer.rt, screenPoint, null, out localPoint))
        {
            thisRT.anchoredPosition = localPoint;
        }
    }
}
