using UnityEngine;

namespace Train.AIConnection.Data
{
    [System.Serializable]
    public class AIData
    {
        public EnemyData[] Solders;
        public EnemyData[] Snipers;
        public EnemyData[] Bombers;
        public EnemyData[] MeleeFighters;
        public EnemyData[] Spawners;
        public PlayerData[] Players;
    }
}