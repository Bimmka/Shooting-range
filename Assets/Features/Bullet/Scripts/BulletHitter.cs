using Features.Bullet.Data;
using Features.Targets.Scripts.Elements;
using UnityEngine;

namespace Features.Bullet.Scripts
{
  public class BulletHitter
  {
    private readonly Transform bulletTransform;
    private readonly BulletHitterSettings settings;
    private readonly RaycastHit2D[] hits;

    private int currentDamage;

    public BulletHitter(Transform bulletTransform, BulletHitterSettings settings)
    {
      this.bulletTransform = bulletTransform;
      this.settings = settings;
      hits = new RaycastHit2D[settings.MaxHitCount];
    }
    
    public void Hit()
    {
      int hitCount = Physics2D.CircleCastNonAlloc(bulletTransform.position, settings.Radius, Vector2.zero, hits, 0, settings.HitMask);

      for (int i = 0; i < hitCount; i++)
      {
        if (hits[i].collider.TryGetComponent(out TargetPresenter targetPresenter))
          targetPresenter.TakeDamage(currentDamage);
      }
    }

    public void SetDamage(int damage) => 
      currentDamage = damage;
  }
}