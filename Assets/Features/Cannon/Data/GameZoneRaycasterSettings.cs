using UnityEngine;

namespace Features.Cannon.Data
{
  [CreateAssetMenu(fileName = "GameZoneRaycasterSettings", menuName = "StaticData/Cannon/Create Game Zone Raycaster Settings", order = 52)]
  public class GameZoneRaycasterSettings : ScriptableObject
  {
    public LayerMask GameZoneMask;
    public int MaxHitCount = 1;
    public float Distance = 10f;
  }
}