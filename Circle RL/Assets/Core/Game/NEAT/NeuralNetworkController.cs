using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Game.Enemy;

public class NeuralNetworkController : MonoBehaviour
{

    [SerializeField] private NeuralNetwork network;
    [SerializeField] private Dictionary<int, int> nodeToIndexMap = new();
    public EnemyType enemyType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeMatrices();
        TestNetwork();
    }

    void TestNetwork()
    {
        //Example Usage: Test Network
        Vector<float> testInput = new Vector<float>(network.numOfInputNodes);
        testInput[0] = 1f;  // Set some input values
        testInput[1] = 0f;
        //testInput[1] = 0.2f;
        //testInput[2] = 0.8f;

        //Activate Network
        Vector<float> output = ActivateNetwork(testInput);

        //Debug output values
        Debug.Log("Network Output: ");
        for (int i = 0; i < output.Length; i++)
        {
            Debug.Log("Output " + network.outputNodes[i] + ": " + output[i]);
        }
    }

    void InitializeMatrices()
    {

        for (int i = -network.numOfInputNodes; i < 0; i++)
        {
            nodeToIndexMap.Add(i, i + network.numOfInputNodes);
        }
        int idx = 0;
        foreach (var item in network.nodes)
        {
            if (item.key < 10) continue;
            nodeToIndexMap.Add(item.key, idx + network.numOfInputNodes);
            idx++;
        }
        foreach (var item in network.nodes)
        {
            if (item.key >= 10) continue;
            nodeToIndexMap.Add(item.key, idx + network.numOfInputNodes);
            idx++;
        }

        int numNodes = network.nodes.Count + network.numOfInputNodes;

        network.weights = new Matrix<float>(numNodes, numNodes);
        network.biases = new Vector<float>(numNodes);



        // Initialize biases
        foreach (var node in network.nodes)
        {
            network.biases[nodeToIndexMap[node.key]] = node.bias;
        }


        // Initialize weights based on enabled connections
        foreach (var connection in network.connections)
        {
            if (connection.enabled)
            {
                network.weights[nodeToIndexMap[connection.outNode], nodeToIndexMap[connection.inNode]] = connection.weight;
            }
        }
    }



    Vector<float> ActivateNetwork(Vector<float> input)
    {
        //Create node values vector
        Vector<float> nodeValues = new Vector<float>(network.weights.Rows);


        //Initialize input nodes with the provided input values
        for (int i = 0; i < network.numOfInputNodes; i++)
        {
            nodeValues[i] = input[i];
        }

        //Go through each node of the network and caclulate their value
        for (int i = 0; i < network.weights.Rows; i++)
        {
            float sum = network.biases[i];

            //Loop through connected nodes and accumulate their values
            for (int j = 0; j < network.weights.Columns; j++)
            {
                sum += nodeValues[j] * network.weights[i, j];
            }
            int key = -1;
            foreach (var item in nodeToIndexMap)
            {
                if (item.Value == i)
                {
                    key = item.Key;
                }
            }

            foreach (var node in network.nodes)
            {
                if (node.key == key)
                {
                    nodeValues[i] = ApplyActivationFunction(sum, node.activation, node.response);
                    break;
                }
            }
        }

        //Extract the network's output values and create output vector
        Vector<float> output = new Vector<float>(network.outputNodes.Count);
        for (int i = 0; i < network.outputNodes.Count; i++)
        {
            output[i] = nodeValues[nodeToIndexMap[network.outputNodes[i]]];
        }

        return output;
    }

    float ApplyActivationFunction(float x, string activation, float response)
    {
        switch (activation)
        {
            case "sigmoid":
                return 1 / (1 + Mathf.Exp(-x * response));
            case "relu":
                return Mathf.Max(0, x * response);
            case "identity":
                return x * response;
            default:
                Debug.LogError("Unknown activation function: " + activation);
                return x; // Or some default value
        }
    }
}
