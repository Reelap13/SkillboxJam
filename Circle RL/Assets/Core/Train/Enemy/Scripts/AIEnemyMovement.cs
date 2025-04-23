using UnityEngine;

namespace Game.Enemy
{
    public class AIEnemyMovement : MonoBehaviour
    {
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
            _rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + movement);
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

    }
}