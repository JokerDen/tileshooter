using UnityEngine;

public class LocalRotator : MonoBehaviour
{
    public Vector3 speed;
    
    void Update()
    {
        var angles = transform.localEulerAngles;
        angles += speed * Time.deltaTime;
        transform.localEulerAngles = angles;
    }
}
