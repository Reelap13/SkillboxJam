using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyCounter : WinCondition
{
    int enemyCounter;

    private void Awake()
    {
        enemyCounter = 0;
        IEnemy.summonEnemy.AddListener(OnEnemySummon);
        IEnemy.destroyEnemy.AddListener(OnEnemyDestroy);
        isWin = false;
    }

    private void OnEnemySummon()
    {
        enemyCounter++;
        isWin = enemyCounter <= 0;
        Debug.Log(enemyCounter);
    }
    private void OnEnemyDestroy()
    {
        enemyCounter--;
        isWin = enemyCounter <= 0;
        Debug.Log(enemyCounter);
    }

}
