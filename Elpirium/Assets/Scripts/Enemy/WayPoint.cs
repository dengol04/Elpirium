using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private DirectionsEnum _newDirection;

    public DirectionsEnum newDirection => _newDirection;

    public void setNewDirection(DirectionsEnum newDirection)
    {
        _newDirection = newDirection;
    }

    public void changeDirection(GameObject somebody)
    {
        if (!somebody.TryGetComponent<IMovable>(out var t))
            throw new System.InvalidOperationException("� ������� ������� �� ����� ���� �����������");

        somebody.GetComponent<IMovable>().Direction = _newDirection;
    }
}
