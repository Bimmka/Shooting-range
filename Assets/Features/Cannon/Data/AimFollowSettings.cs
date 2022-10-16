using UnityEngine;

namespace Features.Cannon.Data
{
  [CreateAssetMenu(fileName = "AimFollowSettings", menuName = "StaticData/Cannon/Create Aim Follow Settings", order = 52)]
  public class AimFollowSettings : ScriptableObject
  {
    public float YRotationLerp;
    public float XRotationLerp;
    public float XRotationShift = 90f;
  }
}