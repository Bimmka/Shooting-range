using UnityEngine;

namespace Features.Input
{
  public class PlayerInputService : IPlayerInputService
  {
    public bool IsFire => UnityEngine.Input.GetMouseButtonDown(0);
    public Vector2 MousePosition => UnityEngine.Input.mousePosition;

  }
}