using Features.Level.Zone.Data;
using UnityEngine;

namespace Features.Level.Zone.Scripts
{
  public class TargetsZoneBoundsMarker : MonoBehaviour
  {
    [SerializeField] private TargetsZoneBoundsSettings settings;

    private void OnDrawGizmos()
    {
      Gizmos.DrawWireCube(settings.CenterPoint, settings.Size);
    }
  }
}