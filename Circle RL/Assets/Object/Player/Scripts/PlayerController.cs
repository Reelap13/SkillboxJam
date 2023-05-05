using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = GetComponent<Transform>();
    }
}
