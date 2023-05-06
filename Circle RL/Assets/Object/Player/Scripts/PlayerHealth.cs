using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public UnityEvent loseHitPoint;
    [SerializeField] int maxHitPoint;
    float hitPoint;
    int invincibles;


    private void Start()
    {
        hitPoint = maxHitPoint;
    }
    public int MaxHitPoint
    {
        get { return maxHitPoint; }
    }

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
            loseHitPoint?.Invoke();
            if (hitPoint <= 0)
            {
                Destroy(gameObject);
                Debug.Log("DEATH");
            }
        }
    }
    
}
