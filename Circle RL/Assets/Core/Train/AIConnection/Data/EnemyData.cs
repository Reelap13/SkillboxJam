using UnityEngine;

namespace Train.AIConnection.Data
{
    [System.Serializable]
    public class EnemyData
    {
        public Sensors Sensors;
        public Coordinates EnemyCoordinates;
        public Coordinates PlayerCoordinates;
        public float HP;
        public PlayerInputType PlayerInputPredict;
        public PlayerWeaponType PlayerWeaponType;
    }
}