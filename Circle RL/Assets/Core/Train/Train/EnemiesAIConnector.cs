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
            Tuple<EnemiesCommands, PlayerCommand[]> commands = EnemyCommandParser.Parse(data, Arenas.Count);
            ProcessEnemyCommand(commands.Item1);
            ProcessPlayersCommand(commands.Item2);
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

            AIData data = new AIData();
            data.Solders = solders_data.ToArray();
            data.Snipers = snipers_data.ToArray();
            data.Bombers = bombers_data.ToArray();
            data.MeleeFighters = melee_fighters_data.ToArray();
            data.Spawners = spawners_data.ToArray();

            PlayerData[] players_data = new PlayerData[Arenas.Count];
            for (int i = 0; i < Arenas.Count; ++i)
                players_data[i] = Arenas[i].PlayerSpawner.GetPlayerData();
            data.Players = players_data;

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

        public void ProcessPlayersCommand(PlayerCommand[] commands)
        {
            for (int i = 0; i < Arenas.Count; ++i)
            {
                Arenas[i].PlayerSpawner.ProcessCommand(commands[i]);
            }
        }
    }
    public static class EnemyCommandParser
    {

        public static Tuple<EnemiesCommands, PlayerCommand[]> Parse(string json, int size)
        {
            json = json.Replace("[", "").Replace("]", "");
            string[] data = json.Split(',');

            EnemiesCommands enemy_commands = new EnemiesCommands();

            for (int i = 0; i < size * 5; i++)
            {
                float x = float.Parse(data[i * 3].Replace(".", ","));
                float y = float.Parse(data[i * 3 + 1].Replace(".", ","));
                float is_attack = float.Parse(data[i * 3 + 2].Replace(".", ","));

                EnemyCommand command = new EnemyCommand
                {
                    Type = GetEnemyType(i, size),
                    Direction = new Coordinates(x, y),
                    IsAttack = is_attack != 0
                };

                switch (command.Type)
                {
                    case Game.Enemy.EnemyType.SOLDER:
                        enemy_commands.Solders.Add(command);
                        break;
                    case Game.Enemy.EnemyType.SNIPER:
                        enemy_commands.Snipers.Add(command);
                        break;
                    case Game.Enemy.EnemyType.BOMBER:
                        enemy_commands.Bombers.Add(command);
                        break;
                    case Game.Enemy.EnemyType.MELEE_FIGHTER:
                        enemy_commands.MeleeFighters.Add(command);
                        break;
                    case Game.Enemy.EnemyType.SPAWNER:
                        enemy_commands.Spawners.Add(command);
                        break;
                }
            }

            PlayerCommand[] player_commands = new PlayerCommand[size];
            for (int i = 0; i < size; ++i)
            {
                int index = size * 5 + i;
                float x = float.Parse(data[index * 3].Replace(".", ","));
                float y = float.Parse(data[index * 3 + 1].Replace(".", ","));
                float is_attack = float.Parse(data[index * 3 + 2].Replace(".", ","));
                float w1 = float.Parse(data[index * 3 + 3].Replace(".", ","));
                float w2 = float.Parse(data[index * 3 + 4].Replace(".", ","));
                float w3 = float.Parse(data[index * 3 + 5].Replace(".", ","));

                PlayerCommand command = new PlayerCommand
                {
                    Direction = new Coordinates(x, y),
                    IsAttack = is_attack != 0,
                    W1 = is_attack != 0,
                    W2 = is_attack != 0,
                    W3 = is_attack != 0
                };

                player_commands[i] = command;
            }



            return new(enemy_commands, player_commands);
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