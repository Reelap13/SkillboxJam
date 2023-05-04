using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static GameCondition;

public class ButtonScript : MonoBehaviour
{
    public UnityEvent buttonPressEvent;
    public void pressButton()
    {
        buttonPressEvent.Invoke();
    }
}
