using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    void Move(float speed, DirectionsEnum direction);

    DirectionsEnum Direction { get; set; }
}
