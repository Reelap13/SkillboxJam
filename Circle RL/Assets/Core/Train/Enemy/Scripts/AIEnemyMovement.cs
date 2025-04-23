using Train.AIConnection.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Enemy
{
    public class AIEnemyMovement : MonoBehaviour
    {
        [field: SerializeField]
        public AIEnemy Enemy { get; private set; }
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

        public float GetDistanceFromPlayer()
        {
            return (Enemy.ArenaController.PlayerSpawner.GetPlayerPosition() - _rb.position).magnitude;
        }
        public Vector2 GetDirectionToPlayer()
        {
            return (Enemy.ArenaController.PlayerSpawner.GetPlayerPosition() - _rb.position).normalized;
        }
    }
}