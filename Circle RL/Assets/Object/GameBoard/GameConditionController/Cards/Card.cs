using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] public string description;
    [SerializeField] public GameObject objectToSpawn;
    public void choose()
    {
        Debug.Log(description);
        ObjectPlacer.Instance.AddObject(objectToSpawn);
    }
}
