using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _ph;

    public void changeHp()
    {
        Debug.Log("HP: " + _ph.HitPoint);
        transform.localScale = new Vector3(_ph.HitPoint / _ph.MaxHitPoint, transform.localScale.y, 1);
    }
}
