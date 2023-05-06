using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    [SerializeField] private Ability abilityQ;
    [SerializeField] private Ability abilityE;
    [SerializeField] private Ability abilityZ;
    [SerializeField] private Ability abilityX;
    [SerializeField] private Ability abilitySpace;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            abilityQ.use();
        else if (Input.GetKeyDown(KeyCode.E))
            abilityE.use();
        else if (Input.GetKeyDown(KeyCode.Z))
            abilityZ.use();
        else if (Input.GetKeyDown(KeyCode.X))
            abilityX.use();
        else if (Input.GetKeyDown(KeyCode.Space))
            abilitySpace.use();
    }
}
