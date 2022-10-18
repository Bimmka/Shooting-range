using System;
using Features.Services.Pause;
using UnityEngine;

namespace Features.Bullet.Scripts
{
  public class BulletModel : IPaused
  {
    private readonly BulletMover mover;
    private readonly BulletHitter hitter;

    public event Action Hitted;

    public BulletModel(BulletMover mover, BulletHitter hitter)
    {
      this.mover = mover;
      this.hitter = hitter;
      this.mover.Reached += OnReachHitPosition;
    }

    public void Cleanup()
    {
      mover.Reached -= OnReachHitPosition;
    }

    public void SetPosition(Vector3 position) => 
      mover.SetPosition(position);

    public void Move(Vector3 startPosition, Vector3 endPosition) => 
      mover.Move(startPosition, endPosition);

    public void Pause() => 
      mover.Stop();

    public void Unpause() => 
      mover.Continue();

    private void OnReachHitPosition()
    {
      hitter.Hit();
      Hitted?.Invoke();
    }

    public void SetDamage(int damage) => 
      hitter.SetDamage(damage);
  }
}