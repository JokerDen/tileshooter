using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class FlexCapsule : MonoBehaviour
{
    [SerializeField] Transform startEnd;
    [SerializeField] Transform otherEnd;
    [SerializeField] Transform connector;

    [SerializeField] float localHeight = 1f;
    [SerializeField] float localRadius = .5f;

    private void Update()
    {
        Vector3 scale;
        Vector3 pos;

        if (connector != null)
        {
            scale = connector.localScale;
            scale.y = localHeight * .5f;
            scale.x = scale.z = localRadius;
            connector.localScale = scale;

            pos = connector.localPosition;
            pos.y = localHeight * .5f;
            connector.localPosition = pos;
        }

        if (otherEnd != null)
        {
            pos = otherEnd.localPosition;
            pos.y = localHeight;
            otherEnd.localPosition = pos;

            scale = otherEnd.localScale;
            scale.Set(localRadius, localRadius, localRadius);
            otherEnd.localScale = scale;
            if (startEnd != null)
                startEnd.localScale = scale;
        }
    }
}
