using Features.Cannon.Data;
using UnityEngine;

namespace Features.Cannon.Scripts.Aim
{
  public class AimFollow
  {
    private readonly Transform upDownRotator; 
    private readonly Transform leftRightRotator;
    private readonly AimFollowSettings settings;

    public AimFollow(Transform upDownRotator, Transform leftRightRotator, AimFollowSettings aimFollowSettings)
    {
      this.upDownRotator = upDownRotator;
      this.leftRightRotator = leftRightRotator;
      settings = aimFollowSettings;
    }

    public void ChangeRotation(Vector3 aimPositionInWorld)
    {
      Vector3 direction = aimPositionInWorld - leftRightRotator.position;
      
      upDownRotator.localRotation = Quaternion.Lerp(upDownRotator.localRotation,UpDownRotation(direction), settings.XRotationLerp);
      leftRightRotator.rotation = Quaternion.Lerp(leftRightRotator.rotation,LeftRightRotation(direction), settings.YRotationLerp);
    }

    private Quaternion UpDownRotation(Vector3 direction)
    {
      Quaternion upRotation = Quaternion.LookRotation(new Vector3(0, -direction.y, direction.z ), Vector3.up);
      upRotation.eulerAngles = new Vector3(0, 0, upRotation.eulerAngles.x - settings.XRotationShift);
      return upRotation;
    }

    private Quaternion LeftRightRotation(Vector3 direction)
    {
      Quaternion leftRightRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);
      leftRightRotation.eulerAngles += settings.YRotationVector * settings.YRotationShift;
      return leftRightRotation;
    }
  }
}