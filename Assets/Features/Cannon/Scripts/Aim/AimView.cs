using UnityEngine;

namespace Features.Cannon.Scripts.Aim
{
  public class AimView : MonoBehaviour
  {
    public void SetPosition(Vector3 positionOnGameZone) => 
      transform.position = new Vector3(positionOnGameZone.x, positionOnGameZone.y, transform.position.z);
  }
}