using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    public UILayer layer;
    private RectTransform thisRT;
    
    public Text label;

    private string dialogText;
    private DialogueData dialogueData;
    private int printIndex;
    private int lineIndex;
    private bool isPrinted;
    public float printInterval;
    private float lastPrintTime;

    public GameObject next;

    private bool terminating;

    public CustomInput input;

    private void Start()
    {
        if (dialogueData == null)
            gameObject.SetActive(false);
    }

    public void Show(DialogueData data)
    {
        terminating = false;
        dialogueData = data;
        lineIndex = 0;
        ResetPrint();
        UpdatePosition();
        gameObject.SetActive(true);
    }

    private void ToNextLine()
    {
        lineIndex++;
        if (lineIndex >= dialogueData.lines.Length)
        {
            ResetPrint();
            terminating = true;
            // dialogueData.Finish();
            // gameObject.SetActive(false);
            return;
        }
        ResetPrint();
    }

    private void ResetPrint()
    {
        isPrinted = false;
        dialogText = dialogueData.GetLine(lineIndex);
        printIndex = 0;
        lastPrintTime = 0f;
        label.text = "";
        next.SetActive(false);
    }

    private void Update()
    {
        if (terminating)
        {
            terminating = false;
            dialogueData.Finish();
            gameObject.SetActive(false);
            return;
        }
        
        // printIndex = Mathf.Min(printIndex, dialogText.Length);
        if (!isPrinted)
        {
            lastPrintTime += Time.deltaTime;
            while (lastPrintTime > printInterval)
            {
                lastPrintTime -= printInterval;
                printIndex++;
            }
            
            isPrinted = printIndex > dialogText.Length;
            if (isPrinted)
            {
                printIndex = dialogText.Length;
                next.SetActive(true);
            }
        }
        
        label.text = dialogText.Substring(0, printIndex);

        if (input.IsAction())
        {
            if (isPrinted)
            {
                ToNextLine();
            }
            else
            {
                printIndex = dialogText.Length;
            }
        }
    }

    private void LateUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (dialogueData == null) return;

        if (thisRT == null)
            thisRT = GetComponent<RectTransform>();
        
        var screenPoint = RectTransformUtility.WorldToScreenPoint(layer.cam, dialogueData.target.position);
        Vector2 localPoint;
        // cam null for overlay canvas
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(layer.rt, screenPoint, null, out localPoint))
        {
            thisRT.anchoredPosition = localPoint;
        }
    }

    public bool IsDialogue(DialogueData arg0)
    {
        return dialogueData == arg0;
    }

    public void ResetAndHide()
    {
        dialogueData = null;
        gameObject.SetActive(false);
    }
}