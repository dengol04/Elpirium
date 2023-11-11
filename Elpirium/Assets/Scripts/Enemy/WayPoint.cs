using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField]
    private Vector2 _newDirection;

    public Vector2 newDirection => _newDirection;

    public void setNewDirection(Vector2 newDirection)
    {
        _newDirection = newDirection;
    }

    public void changeDirection(GameObject somebody)
    {
        if (!somebody.TryGetComponent<IMovable>(out var t))
            throw new System.InvalidOperationException("У данного объекта не может быть направления");

        somebody.GetComponent<IMovable>().Direction = _newDirection;
    }
}
