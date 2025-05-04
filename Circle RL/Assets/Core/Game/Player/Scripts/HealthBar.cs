using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _ph;

    public void changeHp()
    {
        transform.localScale = new Vector3(_ph.HitPoint / _ph.MaxHitPoint, transform.localScale.y, 1);
    }
}
