using Train.AIConnection.Data;
using UnityEngine;

namespace Train.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        public void SpawnPlayer()
        {

        }

        public Vector2 GetPlayerPosition()
        {
            return Vector2.zero;
        }

        public PlayerInputType GetPlayerPredictedInput()
        {
            return PlayerInputType.W;
        }

        public PlayerWeaponType GetPlayerWeaponType()
        {
            return PlayerWeaponType.W1;
        }
    }
}