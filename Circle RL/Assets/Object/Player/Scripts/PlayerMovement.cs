using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;
    private Transform _tr;
    private Vector2 direction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<Transform>();
    }

    private void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
    }

    public void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + direction.normalized * _speed * Time.deltaTime);
    }

}
