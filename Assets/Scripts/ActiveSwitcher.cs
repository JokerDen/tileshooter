using UnityEngine;

public class ActiveSwitcher : MonoBehaviour
{
    public GameObject target;
    public float interval;
    private float switchTime;

    private void Start()
    {
        if (target == this)
            Debug.LogWarning("Can't switch ownself!");
    }

    private void FixedUpdate()
    {
        switchTime += Time.deltaTime;
        if (switchTime >= interval)
        {
            switchTime -= interval;
            target.SetActive(!target.activeSelf);
        }
    }
}