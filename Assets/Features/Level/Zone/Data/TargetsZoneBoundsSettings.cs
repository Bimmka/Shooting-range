using Features.Level.Zone.Scripts;
using UnityEngine;

namespace Features.Level.Zone.Data
{
  [CreateAssetMenu(fileName = "TargetsZoneBoundsSettings", menuName = "StaticData/Targets Zone/Create Targets Zone Settings", order = 52)]
  public class TargetsZoneBoundsSettings : ScriptableObject
  {
    public Vector3 CenterPoint;
    public Vector2 Size;
    public float EdgeWidth = 2f;
    public GameZoneEdge GameZoneEdgePrefab;
    public GameZone GameZonePrefab;
  }
}