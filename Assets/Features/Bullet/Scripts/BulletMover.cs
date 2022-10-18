using System;
using DG.Tweening;
using Features.Bullet.Data;
using UnityEngine;

namespace Features.Bullet.Scripts
{
  public class BulletMover
  {
    private readonly Transform bulletTransform;
    private readonly BulletMoveSettings settings;

    private Tweener moveTweener;

    public event Action Reached;

    public BulletMover(Transform bulletTransform, BulletMoveSettings settings)
    {
      this.bulletTransform = bulletTransform;
      this.settings = settings;
    }
    
    public void SetPosition(Vector3 position) => 
      bulletTransform.position = position;

    public void Move(Vector3 startPosition, Vector3 endPosition)
    {
      Vector3[] movePath = Path(startPosition, endPosition);
      moveTweener = bulletTransform.DOPath(movePath, settings.MoveDuration, settings.PathType).SetEase(settings.MoveEase).OnComplete(OnPathEnd);
    }

    public void Stop() => 
      moveTweener.Pause();

    public void Continue() => 
      moveTweener.Play();

    private Vector3[] Path(Vector3 startPosition, Vector3 endPosition)
    {
      Vector3 middlePoint = (endPosition + startPosition) / 2;
      middlePoint.y += settings.YOffset;
      return new Vector3[] {startPosition, middlePoint, endPosition};
    }

    private void OnPathEnd() => 
      Reached?.Invoke();
  }
}