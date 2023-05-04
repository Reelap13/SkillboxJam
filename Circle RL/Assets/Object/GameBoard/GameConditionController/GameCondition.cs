using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCondition : MonoBehaviour, IResetable
{
    public delegate void WinGamenDelegate();
    static public event WinGamenDelegate winGamenEvent;
    WinCondition[] winConditions;
    bool isItAlreadyWin;
    private void Awake()
    {
        isItAlreadyWin = false;
        winConditions = GetComponents<WinCondition>();
    }


    void FixedUpdate()
    {
        if (isItAlreadyWin)
        {
            return;
        }
        bool isItWin = true;
        foreach(WinCondition winCondition in winConditions)
        {
            isItWin = isItWin && winCondition.IsWin;
        }
        if (isItWin)
        {
            Debug.Log("WIN");
            winGamenEvent?.Invoke();
            isItAlreadyWin = true;
        }


    }
    public void resetObject()
    {
        isItAlreadyWin = false;
    }

}
