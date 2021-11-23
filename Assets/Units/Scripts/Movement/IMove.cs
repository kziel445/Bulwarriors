using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveVelocity
{
    void SetVelocity(Vector2 velocityVector);
}
public interface IMovePosition
{
    void SetMovePosition(Vector2 movePosition);
}
