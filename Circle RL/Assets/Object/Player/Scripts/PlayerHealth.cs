using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    static public UnityEvent loseAllHitPoint;
    [SerializeField] int maxHitPoint;
    float hitPoint;
    int invincibles;

    public bool IsInvincible
    {
        get { return invincibles > 0; }
        set
        {
            if (value)
            {
                invincibles++;
            }
            else
            {
                invincibles--;
            }
        }
    }
    public float HitPoint
    {
        get { return hitPoint; }
        set {
            if (IsInvincible)
            {
                if (value > hitPoint)
                {
                    hitPoint = value;
                    hitPoint = Mathf.Min(maxHitPoint, hitPoint);
                }
                return;
            }
            hitPoint = value;
            hitPoint = Mathf.Min(maxHitPoint, hitPoint);
            if (hitPoint <= 0)
            {
                Debug.Log("DEATH");
                loseAllHitPoint?.Invoke();
            }
        }
    }
    
}
