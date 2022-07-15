using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Weapon weapon;
    
    public enum State
    {
        Default = 0,
        Warned = 50,
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

    public Emotion emotion;

    private void Start()
    {
        StartCoroutine(AI());
    }

    private void SetState(State state)
    {
        this.state = state;
        emotion.Show(state);
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
                    SetState(State.Agro);
                else
                    continue;
            }

            if (target == null)
            {
                target = forwardCast.GetTarget(hostileTeams);
                var angles = transform.localEulerAngles;
                if (target == null)
                {
                    target = rightCast.GetTarget(hostileTeams);
                    if (target != null)
                    {
                        angles.y += 45f;
                    }
                    else
                    {
                        target = leftCast.GetTarget(hostileTeams);
                        if (target != null)
                        {
                            angles.y -= 45f;
                        }
                    }
                }

                transform.localEulerAngles = angles;
            }

            if (target == null)
            {
                var angles = transform.localEulerAngles;
                if (Random.value > .5f)
                    angles.y += 90f;
                else
                    angles.y -= 90f;
                transform.localEulerAngles = angles;
                yield return new WaitForSeconds(1f);
            }

            if (state == State.Warned)
            {
                if (target == null)
                    continue;
                
                SetState(State.Agro);
            }

            if (state == State.Agro)
            {
                yield return new WaitForSeconds(preShotDelay);
            
                weapon.TryShoot();

                if (target != null)
                    target = null;
                else
                    SetState(State.Warned);
            
                yield return new WaitForSeconds(postShotDelay);
                continue;
            }
        }
    }
}
