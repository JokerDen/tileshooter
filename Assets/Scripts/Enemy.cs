using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Weapon weapon;
    
    public enum State
    {
        Default = 0,
    }

    public State state;

    public CastForward forwardCast;
    public CastForward leftCast;
    public CastForward rightCast;

    private void Start()
    {
        StartCoroutine(AI());
    }

    private IEnumerator AI()
    {
        if (state == State.Default)
        {
            
            
            yield return null;
        }
        
        
    }
}
