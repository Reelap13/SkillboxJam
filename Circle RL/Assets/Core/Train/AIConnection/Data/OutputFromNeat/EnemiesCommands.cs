using System.Collections.Generic;
using UnityEngine;

namespace Train.AIConnection.Data
{
    public class EnemiesCommands
    {
        public List<EnemyCommand> Solders;
        public List<EnemyCommand> Snipers;
        public List<EnemyCommand> Bombers;
        public List<EnemyCommand> MeleeFighters;
        public List<EnemyCommand> Spawners;

        public EnemiesCommands()
        {
            Solders = new List<EnemyCommand>();
            Snipers = new List<EnemyCommand>();
            Bombers = new List<EnemyCommand>();
            MeleeFighters = new List<EnemyCommand>();
            Spawners = new List<EnemyCommand>();
        }
    }
}