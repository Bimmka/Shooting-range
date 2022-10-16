using UnityEngine;

namespace Features.Input
{
  public class PlayerInputService : IPlayerInputService
  {
    public bool IsFireClick { get; private set; }
    public Vector2 MousePosition => UnityEngine.Input.mousePosition;

  }
}