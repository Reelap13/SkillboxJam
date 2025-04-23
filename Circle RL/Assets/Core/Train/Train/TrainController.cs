using Train.AIConnection;
using UnityEngine;

namespace Train.Train 
{
    public class TrainController : MonoBehaviour
    {
        [field: SerializeField]
        public ArenasController ArenasController { get; private set; }
        
        [field: SerializeField]
        public IterationsController IterationsController { get; private set; }
        
        [field: SerializeField]
        public ConnectingToNEAT ConnectingToNEAT { get; private set; }

        private void Start()
        {
            InitializeTrain();
        }

        private void InitializeTrain()
        {
            ArenasController.Initialize();
            ConnectingToNEAT.OnCreatingConnection.AddListener(StartTrain);
            ConnectingToNEAT.CreateConnection();
            
        }

        private void StartTrain()
        {
            IterationsController.InitializeNeat();
        }

        private void CreatePopulation()
        {

        }
    }
}