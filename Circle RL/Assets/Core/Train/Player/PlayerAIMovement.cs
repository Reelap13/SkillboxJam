using Train.AIConnection.Data;
using UnityEngine;

namespace Train.Player
{
    public class PlayerAIMovement : MonoBehaviour
    {
        [field: SerializeField]
        public PlayerAI Player { get; private set; }
        [SerializeField] private float _speed = 3f;

        private Rigidbody2D _rb;
        private Vector2 _direction;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 movement = _direction * _speed * Time.deltaTime;
            _rb.MovePosition(_rb.position + movement);
        }

        public void SetDirection(Coordinates direction) => SetDirection(new Vector2(direction.X, direction.Y));
        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
        }

        public Vector2 GetDirectionToClosestEnemy()
        {
            return (Player.ArenaController.EnemySpawner.GetClosestEnemyToPoint(transform.position).position - transform.position).normalized;
        }
        public float GetDistanceToClosestEnemy()
        {
            return (Player.ArenaController.EnemySpawner.GetClosestEnemyToPoint(transform.position).position - transform.position).magnitude;
        }
    }
}