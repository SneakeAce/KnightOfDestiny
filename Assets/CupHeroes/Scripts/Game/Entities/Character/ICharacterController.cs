using System;
using UnityEngine;

public interface ICharacterController : IEntityController
{
    event Action IsCharacterOnPosition;
    void SetPositionToMove(Vector2 position);
}
