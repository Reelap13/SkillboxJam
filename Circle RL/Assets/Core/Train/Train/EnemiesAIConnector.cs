using System.Collections.Generic;
using Train.AIConnection.Data;
using Train.Arena;
using UnityEngine;

namespace Train.Train
{
    public class EnemiesAIConnector : MonoBehaviour
    {
        [field: SerializeField]
        public IterationsController IterationsController { get; private set; }

        public List<ArenaController> Arenas => IterationsController.Controller.ArenasController.Arenas;

        public void SendData()
        {
            string data = GetParsedEnemiesData();

            RequestData request_data = RequestData.GetBuilder().
                SetCommand("Process individuals data").
                SetData(data).
                SetProcessFunction((string data) =>
                {
                    ProcessEnemiesInput(data);
                }).Build();
            IterationsController.NEAT.SendData(request_data);
        }

        private void ProcessEnemiesInput(string data)
        {
            print(data);
        }

        private string GetParsedEnemiesData()
        {
            List<EnemyData> solders_data = new List<EnemyData>();
            List<EnemyData> snipers_data = new List<EnemyData>();
            List<EnemyData> bombers_data = new List<EnemyData>();
            List<EnemyData> melee_fighters_data = new List<EnemyData>();
            List<EnemyData> spawners_data = new List<EnemyData>();
            foreach (var arena in Arenas)
                foreach (var enemy_data in arena.EnemySpawner.GetEnemyData())
                    switch (enemy_data.EnemyType)
                    {
                        case Game.Enemy.EnemyType.SOLDER:
                            solders_data.Add(enemy_data);
                            break;
                        case Game.Enemy.EnemyType.SNIPER:
                            solders_data.Add(enemy_data);
                            break;
                        case Game.Enemy.EnemyType.BOMBER:
                            bombers_data.Add(enemy_data);
                            break;
                        case Game.Enemy.EnemyType.MELEE_FIGHTER:
                            melee_fighters_data.Add(enemy_data);
                            break;
                        case Game.Enemy.EnemyType.SPAWNER:
                            spawners_data.Add(enemy_data);
                            break;
                    }

            EnemiesData data = new EnemiesData();
            data.Solders = solders_data.ToArray();
            data.Snipers = snipers_data.ToArray();
            data.Bombers = bombers_data.ToArray();
            data.MeleeFighters = melee_fighters_data.ToArray();
            data.Spawners = spawners_data.ToArray();

            Debug.Log(JsonUtility.ToJson(data));
            return JsonUtility.ToJson(data);
        }

        public void ProcessEnemyCommand(EnemiesCommands commands)
        {
            for (int i = 0; i < Arenas.Count; ++i)
            {
                EnemySpawner enemies = Arenas[i].EnemySpawner;
                enemies.ProcessCommand(commands.Solders[i]);
                enemies.ProcessCommand(commands.Snipers[i]);
                enemies.ProcessCommand(commands.Bombers[i]);
                enemies.ProcessCommand(commands.MeleeFighters[i]);
                enemies.ProcessCommand(commands.Spawners[i]);
            }
        }
    }
}