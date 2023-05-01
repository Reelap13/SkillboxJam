using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionByTime : MonoBehaviour
{
    [SerializeField] private float _timeToDelete;

    private float _lifetime = 0;

    private void Update()
    {
        _lifetime += Time.deltaTime;
        if (_lifetime > _timeToDelete)
            Destroy(gameObject);
    }
}
