using UnityEngine;

namespace Features.Level.Zone.Scripts
{
  [RequireComponent(typeof(BoxCollider2D))]
  public class SpawnedCollider : MonoBehaviour
  {
    [SerializeField] private BoxCollider2D edgeCollider;

    public void SetPosition(Vector3 position) => 
      transform.position = position;

    public virtual void SetSize(Vector2 size) => 
      edgeCollider.size = size;
  }
}