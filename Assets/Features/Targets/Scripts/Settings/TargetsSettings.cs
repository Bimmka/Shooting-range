using UnityEngine;

namespace Features.Targets.Scripts.Settings
{
  [CreateAssetMenu(fileName = "TargetsSettings", menuName = "StaticData/Target/Create Targets Settings", order = 52)]
  public class TargetsSettings : ScriptableObject
  {
    public TargetSettings[] Settings;
  }
}