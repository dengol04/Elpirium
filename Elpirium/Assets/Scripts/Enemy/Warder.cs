using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warder : Enemy, IMovable
{
    [SerializeField]
    float _speed, _health;
    [SerializeField]
    int _damage;
    [SerializeField]
    Sprite _sprite;
    [SerializeField]
    DirectionsEnum _direction;

    public void Move(float speed, DirectionsEnum direction)
    {

    }

    public override void getDamage(float damage)
    {
        if (damage >= _health)
            Die();
        else
            _health -= damage;
    }

    public override void doDamage(int damage)
    {

    }

    public override void Die()
    {
        Destroy(this);
    }

    public DirectionsEnum Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value;
        }
    }

    public override float Health => _health;

    public override int Damage => _damage;
}
