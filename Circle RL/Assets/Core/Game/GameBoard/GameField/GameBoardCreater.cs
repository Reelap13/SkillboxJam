using Train.AIConnection.Data;
using UnityEngine;

public class GameBoardCreater : MonoBehaviour
{
    [SerializeField] GameObject wall, floar;
    [SerializeField] int n, m;
    [SerializeField] int _r;

    private Vector2 _position;

    public void SummonBoard(Vector2 position)
    {
        _position = position;
        float x_mid = n; // / 2(midl) * 2(one field 2x2 size)
        float y_mid = m;    

        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                GameObject obj;
                int x = 2 * i;
                int y = 2 * j;
                Vector2 dist = new Vector2(x - x_mid, y - y_mid);
                if (dist.x * dist.x + dist.y * dist.y < 4 * r * r)
                    obj = Instantiate(floar);
                else obj = Instantiate(wall);

                obj.transform.position = new Vector3(x + position.x, y + position.y, 1);
                obj.transform.parent = gameObject.transform;
            }
    }

    public Vector2 GetRandomPoint()
    {
        float x_mid = n + _position.x;
        float y_mid = m + _position.y;

        return new Vector2(
            Random.Range((float)-r, (float)r) + x_mid,
            Random.Range((float)-r, (float)r) + y_mid);
    }

    public Coordinates GetParsedPosition(Vector2 position)
    {
        return new Coordinates(new Vector2((position.x - _position.x) / (2 * n), (position.y - _position.y) / (2 * m)));
    }

    public Vector2 GetMiddleOfBoard()
    {
        return new Vector2(n + _position.x, m + _position.y);
    }
    int r
    {
        get { return _r; }
        set { _r = value; }
    }

}