using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[Serializable] struct objectOnBoard
{
    public objectOnBoard(GameObject obj_c, float x_c, float y_c)
    {
        obj = obj_c;
        x = x_c;
        y = y_c;
    }
    [SerializeField] public GameObject obj;
    [SerializeField] public float x, y;
}

public class ObjectPlacer : MonoBehaviour
{

    [SerializeField] int r;
    [SerializeField] float chunkSize;
    bool[,] field;
    [SerializeField] List<objectOnBoard> objects;

    private void Awake()
    {
        field = new bool[2*r, 2*r];
        SpawnObject();
    }
    void SetFieldBorder()
    {
        for (int i = 0; i < 2 * r; i++)
            for (int j = 0; j < 2 * r; j++)
            {
                int x = i - r;
                int y = j - r;
                if (x*x + y*y >= r*r*4)
                {
                    field[x, y] = true;
                }

            }
    }
    void SpawnObject()
    {
        Debug.Log(objects);
        foreach (var obj in objects)
        {
            GameObject t = Instantiate(obj.obj);
            t.transform.position = new Vector3(obj.x, obj.y, 0);
            t.transform.parent = gameObject.transform;
        }
    }
    public void AddObject(GameObject obj)
    {
        int counter = 0;
        for (int i = 0; i < 2 * r; i++)
            for (int j = 0; j < 2 * r; j++)
            {
                if (!field[i,j])
                {
                    counter++;
                }

            }
        if (counter == 0)
        {
            return;
        }
        for (int i = 0; i < 2 * r; i++)
            for (int j = 0; j < 2 * r; j++)
            {
                if (!field[i, j])
                {
                    counter--;
                }
                if (counter <= 0)
                {
                    float x = (i - r) * chunkSize;
                    float y = (j - r) * chunkSize;
                    field[i,j] = true;
                    objects.Add(new objectOnBoard(obj, x, y));

                    return;
                }

            }
    }


    //You can set R only bigger then it was before
    public void setR(int newR)
    {
        int dr = newR - r;
        bool[,] newField = new bool[2 * newR, 2 * newR];
        for (int i = 0; i < 2 * r; i++)
            for (int j = 0; j < 2 * r; j++)
            {
                int x = i - r;
                int y = j - r;
                if (x * x + y * y < r * r * 4)
                {
                    newField[x + dr, y + dr] = field[x, y];
                }

            }
        field = newField;
        SetFieldBorder();
    }

}
