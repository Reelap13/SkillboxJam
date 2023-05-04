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
        IEnemy.summonEnemy += OnEnemySummon;
        IEnemy.destroyEnemy += OnEnemyDestroy;
        isWin = false;
    }

    private void OnEnemySummon()
    {
        enemyCounter++;
    }
    private void OnEnemyDestroy()
    {
        enemyCounter--;
    }

    void Update()
    {
        if (enemyCounter > 0)
        {
            isWin = false;
            return;
        }
        isWin = true;
    }
}
