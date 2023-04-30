using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rb;
    private Transform _tr;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<Transform>();
    }

    private void Update()
    {
        Move();  
    }

    private Vector3 directionToRight = new Vector3(1, 0, 0);
    private Vector3 directionToUp = new Vector3(0, 1, 0);
    private void Move()
    {
        Vector3 direction = new Vector3(0, 0, 0);
        if (Input.GetButton("Horizontal")) 
            direction += (directionToRight * Input.GetAxis("Horizontal")).normalized;
        if (Input.GetButton("Vertical")) 
            direction += (directionToUp * Input.GetAxis("Vertical")).normalized;

        direction = direction.normalized;
        _tr.position = Vector3.MoveTowards(_tr.position, _tr.position + direction * _speed, _speed * Time.deltaTime);
    }

}
