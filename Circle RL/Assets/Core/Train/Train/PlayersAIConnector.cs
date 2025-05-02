using System.Collections.Generic;
using Train.AIConnection.Data;
using Train.Arena;
using UnityEngine;

namespace Train.Train
{
    public class PlayersAIConnector : MonoBehaviour
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
                    ProcessPlayersInput(data);
                }).Build();
            IterationsController.NEAT.SendData(request_data);
        }

        private void ProcessPlayersInput(string data)
        {
            print(data);
        }

        private string GetParsedEnemiesData()
        {
            PlayerData[] players_data = new PlayerData[Arenas.Count];
            for (int i = 0; i < Arenas.Count; ++i)
                players_data[i] = Arenas[i].PlayerSpawner.GetPlayerData();

            Debug.Log(JsonUtility.ToJson(players_data));
            return JsonUtility.ToJson(players_data);
        }

        public void ProcessPlayersCommand(PlayerCommand[] commands)
        {
            for (int i = 0; i < Arenas.Count; ++i)
            {
                Arenas[i].PlayerSpawner.ProcessCommand(commands[i]);
            }
        }
    }
}