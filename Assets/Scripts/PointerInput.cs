using UnityEngine;
using UnityEngine.EventSystems;

public class PointerInput : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Camera castCam;
    
    private bool isOver;

    private Pointable over;

    private void Start()
    {
        castCam = FindObjectOfType<Camera>();
    }

    public void ResetCast()
    {
        over?.SetOut();
        over = null;
    }

    private void Update()
    {
        if (!isOver) return;

        var ray = castCam.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        Pointable castOver = null;
        if (Physics.Raycast(ray, out hit))
            castOver = hit.collider.GetComponent<Pointable>();

        if (castOver != over)
        {
            over?.SetOut();
            over = castOver;
            over?.SetOver();
        }

        if (castOver != null)
        {
            castOver.SetOverHit(hit);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        ResetCast();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            over?.SetDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
