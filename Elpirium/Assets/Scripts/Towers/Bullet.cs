using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidBody;

    [Header("Attributes")]
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private debuffType _debuff;
    [SerializeField] private LayerMask _lMask;

    private Transform _target;
    private static List<Transform> _firedEnemies = new List<Transform>();

    private int _countHowManyEnemiesCanShoot;

    private void Awake()
    {
        _countHowManyEnemiesCanShoot = 3;
        //_firedEnemies = new List<Transform>();
    }

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

    private void findNewTargetForLightning()
    {
        RaycastHit2D[] targets = Physics2D.CircleCastAll(transform.position, 3, transform.position, 0, _lMask);

        Debug.Log("findNewTargetForLightning before Where targets.Length: " + targets.Length);

        targets = targets.Where(x => !_firedEnemies.Contains(x.transform)).ToArray();

        Debug.Log("findNewTargetForLightning after Where targets.Length: " + targets.Length);

        _target = targets.Length >= 1 ? targets[0].transform : null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy en = collision.gameObject.GetComponent<Enemy>();
        if (en)
        {
            _firedEnemies.Add(en.transform);
            en.getDamage(_damage);
            switch (_debuff)
            {
                case debuffType.FROZEN:
                    en.slowed(2);
                    break;
                case debuffType.LIGHTNING:
                    if (--_countHowManyEnemiesCanShoot > 0)
                    {
                        findNewTargetForLightning();
                        if (_target == null)
                            break;
                        return;
                    }
                    break;
                default: break;
            }
            Destroy(gameObject);
        }
    }
}
