using UnityEngine;

namespace Train.AIConnection.Data
{
    [System.Serializable]
    public class Coordinates
    {
        public float X;
        public float Y;

        public Coordinates(Vector2 position) : this(position.x, position.y) { }
        public Coordinates(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}