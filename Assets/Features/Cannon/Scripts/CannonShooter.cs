using Features.Bullet.Scripts;
using Features.Cannon.Data;
using UnityEngine;

namespace Features.Cannon.Scripts
{
  public class CannonShooter
  {
    private readonly BulletsContainer bulletsContainer;
    private readonly Transform shootStartTransform;
    private readonly CannonShooterSettings settings;

    public CannonShooter(BulletsContainer bulletsContainer, Transform shootStartTransform, CannonShooterSettings settings)
    {
      this.bulletsContainer = bulletsContainer;
      this.shootStartTransform = shootStartTransform;
      this.settings = settings;
    }
    
    public void Shoot(Vector3 aimPosition)
    {
      BulletPresenter bullet = bulletsContainer.Bullet();
      bullet.SetPosition(shootStartTransform.position);
      bullet.Show();
      bullet.SetDamage(settings.Damage);
      bullet.Move(shootStartTransform.position, aimPosition);
    }
  }
}