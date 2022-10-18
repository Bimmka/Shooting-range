using UnityEngine;

namespace Features.Cannon.Scripts.Effects
{
  public class ShootEffectPlayer
  {
    private readonly ParticleSystem effect;

    public ShootEffectPlayer(ParticleSystem effect)
    {
      this.effect = effect;
    }

    public void Play()
    {
      if (effect.isPlaying)
        return;
      
      effect.Play();
    }
  }
}