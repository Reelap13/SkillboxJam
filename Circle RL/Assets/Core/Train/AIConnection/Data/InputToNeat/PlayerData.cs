using UnityEngine;

namespace Train.AIConnection.Data
{
    [System.Serializable]
    public class PlayerData
    {

        public Sensors Sensors;
        public Coordinates Position;
        public Coordinates TargetPosition;
        public float HP;
        public PlayerWeaponType CurrentWeapon;
        public float CurrentScore;
        public float MaxScore;
    }
}