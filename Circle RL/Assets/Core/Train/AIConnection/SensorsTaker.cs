using System.Collections;
using UnityEngine;

namespace Train.AIConnection.Data
{
    public class SensorsTaker : MonoBehaviour
    {
        [SerializeField] private float _distance;

        public Sensors GetSensorsData()
        {
            Sensors sensors = new Sensors();

            sensors.D = GetDistanceByDirection(Vector2.down);
            sensors.DL = GetDistanceByDirection(Vector2.down + Vector2.left);
            sensors.L = GetDistanceByDirection(Vector2.left);
            sensors.UL = GetDistanceByDirection(Vector2.up + Vector2.left);
            sensors.U = GetDistanceByDirection(Vector2.up);
            sensors.UR = GetDistanceByDirection(Vector2.up + Vector2.right);
            sensors.R = GetDistanceByDirection(Vector2.right);
            sensors.DR = GetDistanceByDirection(Vector2.down + Vector2.right);

            return sensors;
        }

        public float GetDistanceByDirection(Vector2 direction)
        {
            Vector2 origin = transform.position;
            Vector2 normalized_direction = direction.normalized;

            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, normalized_direction, _distance);
            float distance = _distance;
            Debug.Log(origin + " " + normalized_direction);
            Debug.Log(hits.Length + " " + _distance);
            foreach (var hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject != gameObject)
                {
                    distance = hit.distance;
                    break;
                }
            }

            return 1f / (distance + 1f);   
        }
    }
}