using UnityEngine;

public class FinishDemo : MonoBehaviour
{
    public Trigger trigger;

    private void Start()
    {
        trigger.onTrigger += Finish;
    }

    private void Finish()
    {
        var dayTime = FindObjectOfType<DayTimeUI>(true);
        dayTime.ShowFinish();
    }
}
