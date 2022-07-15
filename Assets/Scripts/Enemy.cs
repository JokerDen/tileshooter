using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Weapon weapon;
    
    public enum State
    {
        Default = 0,
        Agro = 100
    }

    public State state;

    public CastForward forwardCast;
    public CastForward rightCast;
    public CastForward leftCast;

    private Damageable target;

    public float preShotDelay;
    public float postShotDelay;

    public int[] hostileTeams;

    private void Start()
    {
        StartCoroutine(AI());
    }

    private IEnumerator AI()
    {
        while (true)
        {
            yield return null;
            
            if (state == State.Default)
            {
                target = forwardCast.GetTarget(hostileTeams);

                if (target != null)
                    state = State.Agro;
            }

            if (state == State.Agro)
            {
                yield return new WaitForSeconds(preShotDelay);
            
                weapon.TryShoot();
            
                yield return new WaitForSeconds(postShotDelay);
            }
        }
    }
}
