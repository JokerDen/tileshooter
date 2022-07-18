using UnityEngine;
using UnityEngine.Events;

public class DisctractionListener : MonoBehaviour
{
    public DistractionEvent onDistraction = new DistractionEvent();
    
    public void Distract(DistractionSource source)
    {
        onDistraction.Invoke(source);
    }
}

public class DistractionEvent : UnityEvent<DistractionSource>
{
    
}
