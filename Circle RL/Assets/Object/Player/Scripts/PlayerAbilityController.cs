using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    [SerializeField] private Ability abilityQ;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            abilityQ.use();
    }
}
