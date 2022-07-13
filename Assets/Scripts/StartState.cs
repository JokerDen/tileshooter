using UnityEngine;

public class StartState : MonoBehaviour
{
    public GameObject[] states;

    public void SetState(int index)
    {
        states[index].SetActive(true);
    }
}
