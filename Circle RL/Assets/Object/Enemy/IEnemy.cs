using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IEnemy : MonoBehaviour
{

    static public UnityEvent summonEnemy = new UnityEvent();
    static public UnityEvent destroyEnemy = new UnityEvent();
    [SerializeField] bool isAlive = true;
    void Start()
    {
        summonEnemy.Invoke();
    }

    void Update()
    {
        if (!isAlive)
        {
            destroyEnemy.Invoke();
            Destroy(gameObject);
        }
    }
}
