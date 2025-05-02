using Train.AIConnection.Data;
using UnityEngine;

namespace Train.Player
{
    public class PlayerAIWeapon : MonoBehaviour
    {
        [SerializeField] private WeaponAttack _w1;

        private void Awake()
        {
            _w1.IsInputBlock = true;
        }

        public void PerformAttack()
        {
            _w1.ImmitateAttack();
        }


        public PlayerWeaponType Type => PlayerWeaponType.W1;
    }
}