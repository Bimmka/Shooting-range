using DG.Tweening;
using UnityEngine;

namespace Features.Bullet.Data
{
  [CreateAssetMenu(fileName = "BulletMoveSettings", menuName = "StaticData/Bullet/Create Bullet Move Settings", order = 52)]
  public class BulletMoveSettings : ScriptableObject
  {
    public float MoveDuration = 1.5f;
    public float YOffset = 15f;
    public Ease MoveEase;
    public PathType PathType;
  }
}