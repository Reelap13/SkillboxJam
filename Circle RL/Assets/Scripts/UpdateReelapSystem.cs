using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpdateReelapSystem : MonoBehaviour
{
    [HideInInspector] public static UnityEvent Run = new UnityEvent();

    void Update()
    {
        Run.Invoke();
    }
}
