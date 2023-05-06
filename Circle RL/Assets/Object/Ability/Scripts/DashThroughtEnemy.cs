using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashThroughtEnemy : Ability
{

    [SerializeField] float _speed;
    [SerializeField] float _time;
    [SerializeField] float _afterUseInvincibleTime;
    [SerializeField] float _cd;
    [SerializeField] int _damage;
    float lastShoot;

    Collider2D _colider;
    Rigidbody2D _rb;
    PlayerMovement _playerMovement;
    PlayerHealth _playerHealth;
    Vector2 direction;
    bool isInUse;
    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _colider = _rb.GetComponent<Collider2D>();
        _playerHealth = GetComponentInParent<PlayerHealth>();
    }
    public override void use()
    {
        if (lastShoot > 0)
        {
            return;
        }
        Debug.Log(_colider);
        Debug.Log(_playerHealth);
        lastShoot = _cd;
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - _rb.transform.position).normalized;
        StartCoroutine(makeDash());
    }

    IEnumerator makeDash()
    {
        Debug.Log("Use");
        isInUse = true;
        _colider.enabled = false;
        _playerHealth.IsInvincible = true;
        yield return new WaitForSeconds(_time);
        _colider.enabled = true;
        isInUse = false;
        Debug.Log("Unuse");
        yield return new WaitForSeconds(_afterUseInvincibleTime);
        _playerHealth.IsInvincible = false;
    }
    private void FixedUpdate()
    {
        lastShoot -= Time.deltaTime;
        if (isInUse)
        {
            _playerMovement.addMovement(direction * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInUse)
        {
            return;
        }
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().EnemyParameters.TakeDamage(_damage);
        }
    }
}
