using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlow
{
    public float coefficient;
    public float time;
    public EnemySlow(float coefficient, float time, AIEnemyMovement movement)
    {
        this.coefficient = coefficient;
        this.time = time;
        movement.slowdown *= coefficient;
        UpdateReelapSystem.Run.AddListener(SubtractDeltaTime);

        void SubtractDeltaTime()
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                movement.slowdown /= coefficient;
                UpdateReelapSystem.Run.RemoveListener(SubtractDeltaTime);
            }
        }
    }
}
