using UnityEngine;

public interface ICharacterController : IEntityController
{
    void SetPositionToMove(Vector2 position);
}
