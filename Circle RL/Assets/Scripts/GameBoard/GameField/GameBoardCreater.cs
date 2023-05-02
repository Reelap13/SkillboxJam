using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameBoardCreater : MonoBehaviour
{
    [SerializeField] GameObject wall, floar;
    [SerializeField] int n, m;
    [SerializeField] int _r;
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
                if (x * x + y * y < 4 * r * r)
                {
                    obj = Instantiate(floar);
                }
                else
                {
                    obj = Instantiate(wall);
                }
                obj.transform.position = new UnityEngine.Vector3(x, y, 1);

                obj.transform.parent = gameObject.transform;


            }
    }


    int r
    {
        get { return _r; }
        set { _r = value; }
    }

}
