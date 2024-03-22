using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public abstract class Enemy : MonoBehaviour
{
    public abstract float Health { get; }

    public abstract int Damage { get; }

    public abstract void doDamage();
    public abstract void Die();
    public abstract void getDamage(float damage);
    public abstract void setSpeed(float divider);
    public abstract void Win();
}
