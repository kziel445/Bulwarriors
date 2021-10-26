using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveVelocity
{
    void SetVelocity(Vector3 velocityVector);
}
public interface IMovePosition
{
    void SetMovePosition(Vector3 movePosition);
}
