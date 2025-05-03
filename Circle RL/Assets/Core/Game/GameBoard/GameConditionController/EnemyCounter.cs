using Game.Enemy;
using UnityEngine;

public class EnemyCounter : WinCondition
{
    int enemyCounter;

    private void Awake()
    {
        enemyCounter = 0;
        AIEnemy.OnSpawned.AddListener(OnEnemySummon);
        AIEnemy.OnDied.AddListener(OnEnemyDestroy);
        isWin = true;
    }

    private void OnEnemySummon(AIEnemy enemy)
    {
        enemyCounter++;
        isWin = enemyCounter <= 0;
    }
    private void OnEnemyDestroy(AIEnemy enemy)
    {
        enemyCounter--;
        isWin = enemyCounter <= 0;
        Debug.Log(enemyCounter);
    }

}
