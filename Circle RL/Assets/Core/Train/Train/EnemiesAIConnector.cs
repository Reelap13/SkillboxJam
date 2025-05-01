using System;
using System.Collections.Generic;
using Game.Enemy;
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
            ProcessEnemyCommand(EnemyCommandParser.Parse(data, 4));
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
                            snipers_data.Add(enemy_data);
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
    public static class EnemyCommandParser
    {
        public static EnemiesCommands Parse(string json, int size)
        {
            var data = JsonUtility.FromJson<Float3ArrayWrapper>("{\"Items\":" + json + "}").Items;

            EnemiesCommands commands = new EnemiesCommands();

            for (int i = 0; i < data.Length; i++)
            {
                var item = data[i].values;

                EnemyCommand command = new EnemyCommand
                {
                    Type = GetEnemyType(i, size),
                    Direction = new Coordinates(item[0], item[1]),
                    IsAttack = item[2] != 0
                };

                switch (command.Type)
                {
                    case Game.Enemy.EnemyType.SOLDER:
                        commands.Solders.Add(command);
                        break;
                    case Game.Enemy.EnemyType.SNIPER:
                        commands.Snipers.Add(command);
                        break;
                    case Game.Enemy.EnemyType.BOMBER:
                        commands.Bombers.Add(command);
                        break;
                    case Game.Enemy.EnemyType.MELEE_FIGHTER:
                        commands.MeleeFighters.Add(command);
                        break;
                    case Game.Enemy.EnemyType.SPAWNER:
                        commands.Spawners.Add(command);
                        break;
                }
            }

            return commands;
        }

        [Serializable]
        public class Float3
        {
            public float[] values;
        }

        [Serializable]
        public class Float3ArrayWrapper
        {
            public Float3[] Items;
        }

        private static EnemyType GetEnemyType(int i, int size)
        {
            if (i < size)
                return EnemyType.SOLDER;
            if (i < size * 2)
                return EnemyType.SNIPER;
            if (i < size * 3)
                return EnemyType.BOMBER;
            if (i < size * 4)
                return EnemyType.MELEE_FIGHTER;
            else return EnemyType.SPAWNER;
        }
    }
}