using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidBody;

    [Header("Attributes")]
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private debuffType _debuff;

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
        Enemy en = collision.gameObject.GetComponent<Enemy>();
        if (en)
        {
            Destroy(gameObject);  
            en.getDamage(_damage);
            switch (_debuff)
            {
                case debuffType.FROZEN:
                    en.slowed(2);
                    break;
                default: break;
            }
        }
    }
}
