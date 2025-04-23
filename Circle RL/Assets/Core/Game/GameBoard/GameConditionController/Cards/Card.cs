using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] public string description;
    [SerializeField] public GameObject objectToSpawn;
    [SerializeField] int score;
    static public int globaleScore = 0;
    public void choose()
    {
        globaleScore += score;
        Debug.Log(description);
        ObjectPlacer.Instance.AddObject(objectToSpawn);
    }
}
