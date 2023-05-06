using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private GameObject pref;

    private void Awake()
    {
        GameObject obj = Instantiate(pref) as GameObject;
        obj.transform.position = transform.position;
    }
}
