using System.Collections;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    [SerializeField] private float _time = 7f;

    void Start()
    {
        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);    
    }
}
