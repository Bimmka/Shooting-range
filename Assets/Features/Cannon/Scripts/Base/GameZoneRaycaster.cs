using Features.Cannon.Data;
using UnityEngine;

namespace Features.Cannon.Scripts.Base
{
  public class GameZoneRaycaster
  {
    private readonly GameZoneRaycasterSettings settings;
    private readonly Camera mainCamera;
    private readonly RaycastHit2D[] hits;
    private int hitCount;

    public Vector3 GameZonePosition { get; private set; }

    public GameZoneRaycaster(GameZoneRaycasterSettings settings, Camera mainCamera)
    {
      this.settings = settings;
      this.mainCamera = mainCamera;
      hits = new RaycastHit2D[settings.MaxHitCount];
    }

    public void Raycast(Vector2 mousePosition)
    {
      Ray ray = mainCamera.ScreenPointToRay(mousePosition);
      hitCount = Physics2D.RaycastNonAlloc(ray.origin, ray.direction, hits, settings.Distance, settings.GameZoneMask);
      if (IsHitZone()) 
        GameZonePosition = hits[0].point;
    }

    public bool IsHitZone() => 
      hitCount > 0;
  }
}