using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameBoardCreater : MonoBehaviour
{
    [SerializeField] GameObject wall, floar;
    [SerializeField] int n, m;
    [SerializeField] int r;
    void Awake()
    {
        SummonBoard();
    }
    void SummonBoard()
    {

        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                GameObject obj;
                int x = 2 * i - n;
                int y = 2 * j - m;
                if (x * x + y * y <= 4 * r * r)
                {
                    obj = Instantiate(floar);
                }
                else
                {
                    obj = Instantiate(wall);
                }
                //Debug.Log(x + "" + y);
                obj.transform.position = new UnityEngine.Vector3(x, y, 1);
                Debug.Log(obj.transform.position);

                obj.transform.parent = gameObject.transform;


            }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
