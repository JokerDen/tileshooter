using UnityEngine;

public class RoadConnection : MonoBehaviour
{
    public GameObject forwardOnly;
    public GameObject rightOnly;
    public GameObject forwardAndRight;
    public GameObject noForwardAndNoRight;

    public void SetNeighbours(bool hasForward, bool hasRight)
    {
        forwardOnly.SetActive(hasForward && !hasRight);
        rightOnly.SetActive(!hasForward && hasRight);
        forwardAndRight.SetActive(hasForward && hasRight);
        noForwardAndNoRight.SetActive(!hasForward && !hasRight);
    }
}
