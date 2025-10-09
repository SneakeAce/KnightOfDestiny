using System;
using UnityEngine;

public interface ICharacterController : IEntityController
{
    ICharacter Character { get; }

    event Action IsCharacterOnPosition;
    void SetPositionToMove(Vector2 position);
}
