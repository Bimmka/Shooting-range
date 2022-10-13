using System.Collections;
using Features.Level.Zone.Scripts;
using Features.Services.Coroutine;
using UnityEngine;

namespace Features.Targets.Scripts.Elements
{
  public class TargetMover
  {
    private readonly Rigidbody2D presenter;
    private readonly float speed;
    private readonly ICoroutineRunner coroutineRunner;

    private Coroutine moveCoroutine;

    private bool isMoving;
    private Vector2 currentMoveDirection;

    public TargetMover(Rigidbody2D presenter, float speed, ICoroutineRunner coroutineRunner)
    {
      this.presenter = presenter;
      this.speed = speed;
      this.coroutineRunner = coroutineRunner;
    }

    public void StartMove()
    {
      if (moveCoroutine != null)
        return;

      isMoving = true;
      moveCoroutine = coroutineRunner.StartCoroutine(Move());
    }

    public void StopMove() => 
      isMoving = false;

    public void ChangeDirectionByCollision(Vector2 normal) => 
      SetMoveDirection(Vector2.Reflect(currentMoveDirection, normal));

    public void SetMoveDirection(Vector2 moveDirection) => 
      currentMoveDirection = moveDirection;

    public void SetPosition(Vector3 position) => 
      presenter.position = position;

    private IEnumerator Move()
    {
      while (isMoving)
      {
        presenter.MovePosition(NextPosition());
        yield return new WaitForFixedUpdate();
      }
    }

    private Vector2 NextPosition() => 
      presenter.position + currentMoveDirection * speed * Time.fixedDeltaTime;
  }
}