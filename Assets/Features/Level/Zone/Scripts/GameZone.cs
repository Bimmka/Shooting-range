using UnityEngine;

namespace Features.Level.Zone.Scripts
{
  public class GameZone : SpawnedCollider
  {
    [SerializeField] private Transform zonePlane;

    public override void SetSize(Vector2 size)
    {
      zonePlane.localScale = new Vector3(zonePlane.localScale.x * size.x, zonePlane.localScale.y, zonePlane.localScale.z * size.y);
      base.SetSize(size);
    }
  }
}