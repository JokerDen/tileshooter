using UnityEngine;

public class RandomSquareRotation : MonoBehaviour
{
    private void Start()
    {
        var angles = transform.localEulerAngles;
        angles.y += Random.Range(0, 4) * 90f;
        transform.localEulerAngles = angles;
    }
}
