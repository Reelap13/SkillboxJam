using Game.Enemy;
using UnityEngine;

namespace Train.AIConnection.Data
{
    [System.Serializable]
    public class EnemyData
    {
        public EnemyType EnemyType;
        public Sensors Sensors;
        public Coordinates EnemyCoordinates;
        public Coordinates PlayerCoordinates;
        public float HP;
        public PlayerInputType PlayerInputPredict;
        public PlayerWeaponType PlayerWeaponType;
        public float CurrentScore;
        public float MaxScore;
    }
}