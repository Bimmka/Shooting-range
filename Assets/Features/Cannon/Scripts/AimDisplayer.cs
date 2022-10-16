using UnityEngine;

namespace Features.Cannon.Scripts
{
  public class AimDisplayer
  {
    private readonly AimView aim;

    public AimDisplayer(AimView aim)
    {
      this.aim = aim;
      
    }
    public void DisplayAimAt(Vector3 positionOnGameZone) => 
      aim.SetPosition(positionOnGameZone);
  }
}