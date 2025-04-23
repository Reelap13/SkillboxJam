using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    static SceneController instance;
    [SerializeField] List<GameObject> resetables;
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
        foreach (var obj in resetables)
        {
            foreach (var toReset in obj.GetComponents<IResetable>())
            {
                if (toReset != null)
                {
                    toReset.resetObject();
                }
            }
            
        }
        level++;
    }
}