using UnityEngine;

namespace Features.Bullet.Data
{
  [CreateAssetMenu(fileName = "BulletHitterSettings", menuName = "StaticData/Bullet/Create Bullet Hitter Settings", order = 52)]
  public class BulletHitterSettings : ScriptableObject
  {
    public LayerMask HitMask;
    public int MaxHitCount = 3;
    public float Radius = 1f;
  }
}