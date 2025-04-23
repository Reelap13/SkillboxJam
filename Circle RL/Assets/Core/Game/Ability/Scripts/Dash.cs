using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{
    [SerializeField] float _speed;
    [SerializeField] float _time;
    [SerializeField] float _cd;
    float lastShoot;

    Rigidbody2D _rb;
    PlayerMovement _playerMovement;
    Vector2 direction;
    bool isInUse;
    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }
    public override void use()
    {
        if (lastShoot > 0)
        {
            return;
        }
        lastShoot = _cd;
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - _rb.transform.position).normalized;
        Debug.Log(direction);
        Debug.Log(_rb);
        StartCoroutine(makeDash());
    }

    IEnumerator makeDash()
    {
        isInUse = true;
        Debug.Log("Use");
        yield return new WaitForSeconds(_time);
        Debug.Log("Unuse");
        isInUse = false;
    }
    private void FixedUpdate()
    {
        lastShoot -= Time.deltaTime;
        if (isInUse)
        {
            _playerMovement.addMovement(direction * _speed * Time.deltaTime);
        }
    }
}
