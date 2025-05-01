using System;
using Train.AIConnection;
using UnityEngine;
using UnityEngine.Events;

namespace Train.Train
{
    public class IterationsController : MonoBehaviour
    {
        [NonSerialized] public UnityEvent OnStartingIteration = new UnityEvent();
        [NonSerialized] public UnityEvent OnEndingIteration = new UnityEvent();
        [NonSerialized] public UnityEvent OnFinishingAlgorithm = new UnityEvent();

        [field: SerializeField]
        public TrainController Controller{ get; private set; }
        [SerializeField] private PlayersAIConnector _players_connector;
        [SerializeField] private EnemiesAIConnector _enemies_connector;
        [SerializeField] private float _iterations_number_per_second = 1f;

        private float _sending_data_time, _time_between_sending_data;

        public ConnectingToNEAT NEAT=> Controller.ConnectingToNEAT;
        public ArenasController ArenasController => Controller.ArenasController;

        private bool _is_population_active = false;

        private void Awake()
        {
            _sending_data_time = 0;
            _time_between_sending_data = 1 / _iterations_number_per_second;
        }

        public void InitializeNeat()
        {
            RequestData request_data = RequestData.GetBuilder().
                SetCommand("Initialize algorithm").
                SetProcessFunction((string response) => CreatePopulations()).
                SetData("[]").Build();
            NEAT.SendData(request_data);
        }

        private void CreatePopulations()
        {
            RequestData request_data = RequestData.GetBuilder().
                SetCommand("Create circles").
                SetProcessFunction((string response) => StartNextIteration()).
                SetData("[]").Build();
            NEAT.SendData(request_data);
        }

        private void StartNextIteration()
        {
            RequestData request_data = RequestData.GetBuilder().
                SetCommand("Run algorithm").
                SetProcessFunction((string response) => { _is_population_active = true; Debug.Log("Run algorithm"); }).
                SetData("[]").Build();
            NEAT.SendData(request_data);
        }

        private void Update()
        {
            if (!_is_population_active)
                return;

            _sending_data_time += Time.deltaTime;
            TrySendData();
        }

        private void TrySendData()
        {
            if (_sending_data_time < _time_between_sending_data)
                return;

            _sending_data_time = 0;

            _players_connector.SendData();
            _enemies_connector.SendData();
        }

    }
}