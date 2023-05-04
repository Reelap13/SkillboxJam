using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : MonoBehaviour
{

    public delegate void SummonEnemy();
    static public event SummonEnemy summonEnemy;
    public delegate void DestroyEnemy();
    static public event DestroyEnemy destroyEnemy;
    void Start()
    {
        summonEnemy.Invoke();
    }

    private void OnDestroy()
    {
        destroyEnemy.Invoke();
    }
    void Update()
    {
        
    }
}
