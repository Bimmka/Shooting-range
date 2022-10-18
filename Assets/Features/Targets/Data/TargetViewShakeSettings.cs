using UnityEngine;

namespace Features.Targets.Data
{
  [CreateAssetMenu(fileName = "TargetViewShakeSettings", menuName = "StaticData/Target/Create Shake Settings", order = 52)]
  public class TargetViewShakeSettings : ScriptableObject
  {
    public float Duration = 1f;
    public float Strength = 0.3f;
    public int Vibrato = 5;
  }
}