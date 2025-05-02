using UnityEngine;

namespace Train.AIConnection.Data
{
    [System.Serializable]
    public class PlayerData
    {

        public Sensors Sensors;
        public Coordinates PlayerCoordinates;
        public Coordinates ClosestEnemyCoordinates;
        public float HP;
        public PlayerWeaponType WeaponType;
        public float CurrentScore;
        public float MaxScore;
    }
}