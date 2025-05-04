using System.Collections.Generic;
using Train.AIConnection.Data;
using UnityEngine;

public class NeurallNetworks : MonoBehaviour
{
    [SerializeField] private List<NeuralNetworkController> _nns;

    public EnemyCommand ProcessEnemyData(EnemyData data)
    {
        foreach (var nn in _nns)
            if (data.EnemyType == nn.enemyType)
                return nn.ActivateNetwork(data);
        return null;
    }
}
