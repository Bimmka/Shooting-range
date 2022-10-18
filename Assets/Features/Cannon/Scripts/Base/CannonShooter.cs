using Features.Bullet.Scripts;
using Features.Cannon.Data;
using Features.Cannon.Scripts.Effects;
using UnityEngine;

namespace Features.Cannon.Scripts.Base
{
  public class CannonShooter
  {
    private readonly BulletsContainer bulletsContainer;
    private readonly Transform shootStartTransform;
    private readonly CannonShooterSettings settings;
    private readonly ShootEffectPlayer shootEffectPlayer;

    public CannonShooter(BulletsContainer bulletsContainer, Transform shootStartTransform,
      CannonShooterSettings settings, ShootEffectPlayer shootEffectPlayer)
    {
      this.bulletsContainer = bulletsContainer;
      this.shootStartTransform = shootStartTransform;
      this.settings = settings;
      this.shootEffectPlayer = shootEffectPlayer;
    }
    
    public void Shoot(Vector3 aimPosition)
    {
      BulletPresenter bullet = bulletsContainer.Bullet();
      bullet.SetPosition(shootStartTransform.position);
      bullet.Show();
      bullet.SetDamage(settings.Damage);
      bullet.Move(shootStartTransform.position, aimPosition);
      shootEffectPlayer.Play();
    }
  }
}