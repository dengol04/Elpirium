using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] LayerMask _enemyMask;
    [SerializeField] GameObject _bulletPref;

    [Header("Attributes")]
    [SerializeField] float _damage;
    [SerializeField] float _attackSpeed; // bps
    [SerializeField] float _range;

    public float range => _range;

    private Transform _target;
    private float _timeUntilShoot;

    private void Awake()
    {
        _target = null;
        _timeUntilShoot = 0;
    }

    private void findNewTarget()
    {
        RaycastHit2D[] targets = Physics2D.CircleCastAll(transform.position, _range, transform.position, 0, _enemyMask);

        _target = targets.Length >= 1 ? targets[0].transform : null;
    }

    private void Update()
    {
        if (_target == null)
        {
            findNewTarget();
            return;
        }

        if (!isInRange())
            _target = null;
        else
        {
            _timeUntilShoot += Time.deltaTime;

            if (_timeUntilShoot >= 1 / _attackSpeed)
            {
                Shoot();
                _timeUntilShoot = 0;
            }

        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, _range);
    }
#endif

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPref, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().setTarget(_target);
    }

    private bool isInRange()
    {
        return Vector2.Distance(_target.transform.position, transform.position) <= _range;
    }
}
