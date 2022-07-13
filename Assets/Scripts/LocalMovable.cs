using DG.Tweening;
using UnityEngine;

public class LocalMovable : MonoBehaviour
{
    public Vector3 offset;
    public float duration;
    public Ease ease;

    public void PlayFromOffset()
    {
        transform.DOKill();
        transform.localPosition = offset;
        transform.DOLocalMove(Vector3.zero, duration).SetEase(ease);
    }
}
