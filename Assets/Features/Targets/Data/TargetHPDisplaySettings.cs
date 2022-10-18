using UnityEngine;

namespace Features.Targets.Data
{
  [CreateAssetMenu(fileName = "TargetHPDisplaySettings", menuName = "StaticData/Target/Create HP Display Settings", order = 52)]
  public class TargetHPDisplaySettings : ScriptableObject
  {
    public float ShowDuration = 1f;
    public float DisappearDuration = 1f;
    public float AppearDuration = 0.5f;
    public float ValueChangeDuration = 0.5f;
  }
}