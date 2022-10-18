using DG.Tweening;
using UnityEngine;

namespace Features.Targets.Data
{
  [CreateAssetMenu(fileName = "TargetDissolveSettings", menuName = "StaticData/Target/Create Dissolve Settings", order = 52)]
  public class TargetDissolveSettings : ScriptableObject
  {
    public float Duration;
    public float MinAlphaClipValue = 0f;
    public float MaxAlphaClipValue = 1.5f;
    public string AlphaClipParameterName;
    public Ease AnimationEase;
  }
}