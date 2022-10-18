using Features.Level.Zone.Data;
using UnityEngine;

namespace Features.Targets.Scripts.Base
{
  public class TargetSpawnPositionCalculator
  {
    private readonly TargetsZoneBoundsSettings boundsSettings;
    private readonly Transform targetSpawnParent;

    public TargetSpawnPositionCalculator(TargetsZoneBoundsSettings boundsSettings, Transform targetSpawnParent)
    {
      this.boundsSettings = boundsSettings;
      this.targetSpawnParent = targetSpawnParent;
    }

    public Vector3 RandomPosition()
    {
      Vector2 halfSize = boundsSettings.Size / 2;
      float x = Random.Range(-halfSize.x + boundsSettings.EdgeWidth, halfSize.x -  boundsSettings.EdgeWidth);
      float y = Random.Range(-halfSize.y + boundsSettings.EdgeWidth, halfSize.y -  boundsSettings.EdgeWidth);
      return new Vector3(x,y,targetSpawnParent.position.z);
    }
  }
}