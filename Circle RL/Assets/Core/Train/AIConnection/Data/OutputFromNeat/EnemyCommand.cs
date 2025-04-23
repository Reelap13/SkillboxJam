using Game.Enemy;
using UnityEngine;

namespace Train.AIConnection.Data
{
    public class EnemyCommand
    {
        public EnemyType Type;
        public Coordinates Direction;
        public bool IsAttack;
    }
}