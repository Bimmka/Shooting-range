using UnityEngine;

namespace Features.Cannon.Data
{
  [CreateAssetMenu(fileName = "CannonShooterSettings", menuName = "StaticData/Cannon/Create Cannon Shooter Settings", order = 52)]
  public class CannonShooterSettings : ScriptableObject
  {
    public int Damage;
  }
}