using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
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

public class ObjectPlacer : MonoBehaviour, IResetable
{
    [SerializeField] int r;
    [SerializeField] float chunkSize;
    bool[,] field;
    [SerializeField] List<objectOnBoard> objects;
    static public ObjectPlacer instance;

    


    static public ObjectPlacer Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        SpawnObject();
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Debug.Log("New object placer was created");
        instance = this;
        field = new bool[2 * r, 2 * r];
        SetFieldBorder();
    }
    void SetFieldBorder()
    {
        Debug.Log("Set field border");
        for (int i = 0; i < 2 * r; i++)
            for (int j = 0; j < 2 * r; j++)
            {
                int x = i - r;
                int y = j - r;


                if (x*x + y*y >= (r - 0.5) * (r - 0.5) || (x == 0 && y == 0))
                {
                    field[i, j] = true;



                }
                //*
                else
                {
                    GameObject t;

                    t = Instantiate(objects[0].obj);
                    t.transform.parent = gameObject.transform;
                    t.transform.position = new Vector3(chunkSize * x, chunkSize * y, 0);
                }//*/
            }
    }
    void SpawnObject()
    {
        foreach (var obj in objects)
        {
            GameObject t = Instantiate(obj.obj);
            t.transform.parent = gameObject.transform;
            t.transform.position = new Vector3(obj.x, obj.y, 0);
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
        counter = UnityEngine.Random.Range(1, counter+1);
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
    public void resetObject()
    {
        foreach (Transform obj in transform)
        {
            Destroy(obj.gameObject);
        }
        SpawnObject();
        
    }

}
