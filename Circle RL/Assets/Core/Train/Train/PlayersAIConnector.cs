using UnityEngine;

namespace Train.Train
{
    public class PlayersAIConnector : MonoBehaviour
    {
        [field: SerializeField]
        public IterationsController IterationsController { get; private set; }

        public void SendData()
        {
            string data = "";

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
    }
}