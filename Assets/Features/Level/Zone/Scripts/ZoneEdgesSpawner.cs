using Features.Level.Zone.Data;
using Features.Services.Assets;
using UnityEngine;

namespace Features.Level.Zone.Scripts
{
  public class ZoneEdgesSpawner
  {
    private readonly IAssetProvider assetProvider;
    private readonly TargetsZoneBoundsSettings settings;
    private readonly Transform spawnParent;

    public ZoneEdgesSpawner(IAssetProvider assetProvider, TargetsZoneBoundsSettings settings, Transform spawnParent)
    {
      this.assetProvider = assetProvider;
      this.settings = settings;
      this.spawnParent = spawnParent;
    }

    public void SpawnEdges()
    {
      SpawnTopEdge();
      SpawnLeftEdge();
      SpawnRightEdge();
      SpawnBottomEdge();
    }

    private void SpawnTopEdge()
    {
      InitializeEdge(
        Edge(), 
        new Vector3(settings.CenterPoint.x, settings.Size.y / 2), 
        new Vector2(settings.Size.x, settings.EdgeWidth)
        );
    }
    
    private void SpawnLeftEdge()
    {
      InitializeEdge(
        Edge(), 
        new Vector3(-settings.Size.x / 2, settings.CenterPoint.y), 
        new Vector2(settings.EdgeWidth, settings.Size.y)
      );
    }
    
    private void SpawnRightEdge()
    {
      InitializeEdge(
        Edge(), 
        new Vector3(settings.Size.x / 2, settings.CenterPoint.y), 
        new Vector2(settings.EdgeWidth, settings.Size.y)
      );
    }
    
    private void SpawnBottomEdge()
    {
      InitializeEdge(
        Edge(), 
        new Vector3(settings.CenterPoint.x, - settings.Size.y / 2), 
        new Vector2(settings.Size.x, settings.EdgeWidth)
      );
    }

    private void InitializeEdge(ZoneEdge edge, Vector3 position, Vector2 size)
    {
      edge.SetPosition(position);
      edge.SetEdgeSize(size);
    }

    private ZoneEdge Edge() => 
      assetProvider.Instantiate(settings.Prefab, spawnParent);
  }
}