using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidBody;

    [Header("Attributes")]
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private Transform _target;

    public void setTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null)
            return;

        Vector2 bulletDirection = (_target.position - transform.position).normalized;

        _rigidBody.velocity = bulletDirection * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Enemy>().getDamage(_damage);
        }
    }
}
