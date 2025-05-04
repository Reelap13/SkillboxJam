using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace Train.Player
{
    public class InputPredictor : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _updated_time = 0.2f;
        [SerializeField] private float _max_sequence_length = 100;

        private List<Vector2> _inputs = new();

        private List<Vector2> _directions = new List<Vector2>
        {
            new Vector2(0, 1),
            new Vector2(-1, 1),
            new Vector2(-1, 0),
            new Vector2(-1, -1),
            new Vector2(0, -1),
            new Vector2(1, -1),
            new Vector2(1, 0),
            new Vector2(1, 1),
        };

        private void Awake()
        {
            StartCoroutine(TakeTargetUpdates());
        }

        private IEnumerator TakeTargetUpdates()
        {
            while (true)
            {
                _inputs.Add(_target.position);
                if (_inputs.Count > _max_sequence_length)
                    _inputs.RemoveAt(0);
                yield return new WaitForSeconds(_updated_time);
            }
        }

        public Vector2 GetPredictedDirection()
        {
            if (_inputs.Count < 4)
                return GetRandomDirection();

            Vector2[] directions = CalculatePredictedDirection();
            Dictionary<Vector2[], float[]> frequency = ComputeFrequency(directions);

            Vector2 predict_direction = PredictDirection(directions, frequency);
            return predict_direction;
        }

        private Vector2[] CalculatePredictedDirection()
        { 
            Vector2[] directions = new Vector2[_inputs.Count - 1];
            for (int i = 1; i < _inputs.Count; i++)
            {
                Vector2 previous = _inputs[i - 1];
                Vector2 current = _inputs[i];
                directions[i - 1] = GetClosestDirection(current - previous);
            }
            return directions;
        }

        private Dictionary<Vector2[], float[]> ComputeFrequency(Vector2[] directions)
        {
            Dictionary<Vector2[], float[]> frequency = new();
            float size = directions.Length;
            for (int i = 2; i < size; ++i)
            {
                Vector2[] previous = new Vector2[2]
                {
                    directions[i - 2],
                    directions[i - 1],
                };
                Vector2 current = directions[i];
                if (!frequency.ContainsKey(previous))
                    frequency[previous] = new float[8] { 0, 0, 0, 0, 0, 0, 0, 0};

                int direction_index = GetClosestDirectionIndex(current);
                frequency[previous][direction_index] += 1;
            }

            return frequency;
        }

        private Vector2 PredictDirection(Vector2[] directions, Dictionary<Vector2[], float[]> frequency)
        {
            Vector2[] previous = new Vector2[2]
            {
                    directions[directions.Length - 3],
                    directions[directions.Length - 2],
            };

            if (!frequency.ContainsKey(previous))
                return GetRandomDirection();

            float[] probabilities = frequency[previous];
            int max_index = 0;
            for (int i = 0; i < probabilities.Length; ++i)
                if (probabilities[i] > probabilities[max_index])
                    max_index = i;

            return _directions[max_index].normalized;
        }

        private Vector2 GetClosestDirection(Vector2 input_direction)
        {
            float closest_angle = float.MaxValue;
            Vector2 closest_direction = Vector2.zero;
            foreach (var direction in _directions)
            {
                float angle = (input_direction.normalized - direction.normalized).magnitude;
                if (angle < closest_angle)
                {
                    closest_angle = angle;
                    closest_direction = direction;
                }
            }
            return closest_direction.normalized;
        }

        private int GetClosestDirectionIndex(Vector2 input_direction)
        {
            float closest_angle = float.MaxValue;
            int closest_direction_index = -1;
            for (int i = 0; i < _directions.Count; ++i)
            {
                Vector2 direction = _directions[i];
                float angle = (input_direction.normalized - direction.normalized).magnitude;
                if (angle < closest_angle)
                {
                    closest_angle = angle;
                    closest_direction_index = i;
                }
            }
            Debug.Log(closest_direction_index);
            return closest_direction_index;
        }

        private Vector2 GetRandomDirection()
        {
            return _directions[Random.Range(0, _directions.Count)];
        }
    }
}