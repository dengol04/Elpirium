using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private float _damage;

    public void setTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
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
