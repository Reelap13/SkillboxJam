using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public class NeatNetworkLoader : MonoBehaviour
{
    public string filePath = "neat_network.txt"; // Replace with your file path

    private NeuralNetwork network;
    Dictionary<int, int> nodeToIndexMap = new();

    [System.Serializable]
    public class NodeData
    {
        public int key;
        public float bias;
        public float response;
        public string activation;
        public string aggregation;

        public NodeData(int key, float bias, float response, string activation, string aggregation)
        {
            this.key = key;
            this.bias = bias;
            this.response = response;
            this.activation = activation;
            this.aggregation = aggregation;
        }
    }

    [System.Serializable]
    public class ConnectionData
    {
        public int inNode;
        public int outNode;
        public float weight;
        public bool enabled;

        public ConnectionData(int inNode, int outNode, float weight, bool enabled)
        {
            this.inNode = inNode;
            this.outNode = outNode;
            this.weight = weight;
            this.enabled = enabled;
        }
    }

    [System.Serializable]
    public class NeuralNetwork
    {
        public Dictionary<int, NodeData> nodes = new Dictionary<int, NodeData>();
        public List<ConnectionData> connections = new List<ConnectionData>();
        public List<int> inputNodes = new List<int>();
        public List<int> outputNodes = new List<int>();

        public Vector<float> biases;
        public Matrix<float> weights;
    }

    public enum ActivationFunction
    {
        Sigmoid,
        Tanh,
        ReLU,
        Identity,
        Softmax,
        None
    }

    public struct Vector<T>
    {
        public T[] data;

        public Vector(int size)
        {
            data = new T[size];
        }

        public int Length
        {
            get { return data.Length; }
        }

        public T this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }
    }

    public struct Matrix<T>
    {
        public T[,] data;

        public Matrix(int rows, int cols)
        {
            data = new T[rows, cols];
        }

        public int Rows
        {
            get { return data.GetLength(0); }
        }

        public int Columns
        {
            get { return data.GetLength(1); }
        }

        public T this[int row, int col]
        {
            get { return data[row, col]; }
            set { data[row, col] = value; }
        }
    }

    void Start()
    {
        network = LoadNetworkFromFile(filePath);

        foreach (var item in network.nodes)
        {
            Debug.Log(item.Key);
            Debug.Log(item.Value.bias);
        }

        if (network != null)
        {
            Debug.Log("Network loaded successfully.");

            //Initialize matrices
            InitializeMatrices();

            //Test the network with sample input
            TestNetwork();
        }
        else
        {
            Debug.LogError("Failed to load network.");
        }
    }

    NeuralNetwork LoadNetworkFromFile(string path)
    {
        NeuralNetwork network = new NeuralNetwork();

        try
        {
            string[] lines = File.ReadAllLines(path);
            Dictionary<int, NodeData> nodes = new Dictionary<int, NodeData>();
            List<ConnectionData> connections = new List<ConnectionData>();

            // Parsing Nodes
            bool parsingNodes = false;
            bool parsingConnections = false;
            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("Nodes:"))
                {
                    parsingNodes = true;
                    parsingConnections = false;
                    continue;
                }
                else if (trimmedLine.StartsWith("Connections:"))
                {
                    parsingNodes = false;
                    parsingConnections = true;
                    continue;
                }

                if (parsingNodes && !string.IsNullOrEmpty(trimmedLine))
                {
                    ParseNodeLine(trimmedLine, nodes);
                }

                if (parsingConnections && !string.IsNullOrEmpty(trimmedLine))
                {
                    ParseConnectionLine(trimmedLine, connections);
                }
            }

            network.nodes = nodes;
            network.connections = connections;

            // Identify Input and Output Nodes
            foreach (var node in network.nodes)
            {
                if (node.Key < 0)
                {
                    network.inputNodes.Add(node.Key);
                }
                else if (node.Key >= 0 && node.Key <= 2)
                {
                    network.outputNodes.Add(node.Key);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error loading network: " + e.Message);
            return null;
        }

        return network;
    }

    void ParseNodeLine(string line, Dictionary<int, NodeData> nodes)
    {
        //Example: 0 DefaultNodeGene(key=0, bias=-1.1970967437816664, response=1.0, activation=tanh, aggregation=sum)
        string[] parts = line.Split('(');


        if (parts.Length > 1)
        {
            // key=0, bias=-1.1970967437816664, response=1.0, activation=tanh, aggregation=sum)
            string[] properties = parts[1].Replace(")", "").Split(',');

            int key = int.Parse(properties[0].Split('=')[1]);
            float bias = float.Parse(properties[1].Split('=')[1].Replace('.', ','));
            float response = float.Parse(properties[2].Split('=')[1].Replace('.', ','));
            string activation = properties[3].Split('=')[1].Trim();
            string aggregation = properties[4].Split('=')[1].Trim();

            NodeData nodeData = new NodeData(key, bias, response, activation, aggregation);
            nodes.Add(key, nodeData);
        }
    }

    void ParseConnectionLine(string line, List<ConnectionData> connections)
    {
        // Example: DefaultConnectionGene(key=(-18, 0), weight=-0.3216750879452143, enabled=True)
        string[] parts = line.Split('(');
        if (parts.Length > 1)
        {
            string properties = parts[2].Replace(")", "").Replace(" ", "").Replace("weight=", "").Replace("enabled=", "");

            string[] values = properties.Split(',');


            int inNode = int.Parse(values[0]);
            int outNode = int.Parse(values[1]);

            float weight = float.Parse(values[2].Replace(".", ","));
            bool enabled = bool.Parse(values[3]);

            ConnectionData connectionData = new ConnectionData(inNode, outNode, weight, enabled);
            connections.Add(connectionData);
        }
    }

    void InitializeMatrices()
    {
        int minNodeIndex = 0;

        foreach (var item in network.connections)
        {
            if (item.inNode < minNodeIndex)
            {
                minNodeIndex = item.inNode;
            }
        }
        int inputNodes = Mathf.Abs(minNodeIndex);

        for (int i = minNodeIndex; i < 0; i++)
        {
            nodeToIndexMap.Add(i, i + inputNodes);
        }
        int idx = 0;
        foreach (var item in network.nodes)
        {
            nodeToIndexMap.Add(item.Key, idx + inputNodes);
            idx++;
        }

        int numNodes = network.nodes.Keys.Count + inputNodes;

        network.weights = new Matrix<float>(numNodes, numNodes);
        network.biases = new Vector<float>(numNodes);



        // Initialize biases
        foreach (var node in network.nodes)
        {
            network.biases[nodeToIndexMap[node.Key]] = node.Value.bias;
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
        for (int i = 0; i < Mathf.Abs(nodeToIndexMap.Keys.Min()); i++)
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
                if(item.Value == i)
                {
                    key = item.Key;
                }
            }

            //Apply the activation function
            if (key != -1 && network.nodes.ContainsKey(key))
            {
                nodeValues[i] = ApplyActivationFunction(sum, network.nodes[key].activation, network.nodes[key].response);
            }
        }

        //Extract the network's output values and create output vector
        Vector<float> output = new Vector<float>(network.outputNodes.Count);
        for (int i = 0; i < network.outputNodes.Count; i++)
        {
            output[i] = nodeValues[nodeToIndexMap[network.outputNodes[i]]];
        }

        for (int i = 0; i < network.weights.Columns; i++)
        {
            string s = "";
            for (int j = 0; j < network.weights.Rows; j++)
            {
                s += network.weights[j, i].ToString();
                s += " ";
            }
            Debug.Log(s);
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

    void TestNetwork()
    {
        //Example Usage: Test Network
        Vector<float> testInput = new Vector<float>(Mathf.Abs(nodeToIndexMap.Keys.Min()));
        testInput[0] = 0.5f;  // Set some input values
        testInput[1] = 0.2f;
        testInput[2] = 0.8f;

        //Activate Network
        Vector<float> output = ActivateNetwork(testInput);

        //Debug output values
        Debug.Log("Network Output: ");
        for (int i = 0; i < output.Length; i++)
        {
            Debug.Log("Output " + network.outputNodes[i] + ": " + output[i]);
        }
    }
}