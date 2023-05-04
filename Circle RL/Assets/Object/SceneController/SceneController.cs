using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    static SceneController instance;
    static public SceneController Instance
    {
        get { return instance; }
    }

    int level;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        level = 0;
        instance = this;
    }
    public void loadNextScene()
    {
        level++;
    }
}