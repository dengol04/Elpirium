using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    float _damage;
    [SerializeField]
    float _attackSpeed; // bps
    [SerializeField]
    float _range;
    [SerializeField]
    LayerMask _enemyMask;

    private GameObject _target;

   // private void findTarget()
    //{
        //RaycastHit2D[] targets = Physics2D.CircleCastAll(transform.position, _range, transform.position, 0, enemyMask)
   // }

}
