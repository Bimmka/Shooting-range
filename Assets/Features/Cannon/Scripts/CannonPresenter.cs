using Features.Bullet.Scripts;
using Features.Cannon.Data;
using Features.Input;
using UnityEngine;
using Zenject;

namespace Features.Cannon.Scripts
{
  public class CannonPresenter : MonoBehaviour
  {
    [SerializeField] private Transform upDownRotation;
    [SerializeField] private Transform leftRightRotation;
    [SerializeField] private Transform bulletStartPosition;
    [SerializeField] private AimFollowSettings aimFollowSettings;
    [SerializeField] private GameZoneRaycasterSettings gameZoneRaycasterSettings;
    [SerializeField] private CannonShooterSettings shooterSettings;

    private CannonModel model;

    [Inject]
    public void Construct([Inject(Id = "Main Camera")] Camera mainCamera, IPlayerInputService playerInputService, AimView aimView, BulletsContainer bulletsContainer)
    {
      AimDisplayer aimDisplayer = new AimDisplayer(aimView);
      AimFollow aimFollow = new AimFollow(upDownRotation, leftRightRotation, aimFollowSettings);
      GameZoneRaycaster gameZoneRaycaster = new GameZoneRaycaster(gameZoneRaycasterSettings, mainCamera);
      CannonShooter cannonShooter = new CannonShooter(bulletsContainer, bulletStartPosition, shooterSettings);
      model = new CannonModel(gameZoneRaycaster, aimDisplayer, aimFollow, playerInputService, cannonShooter);
    }

    private void Update()
    {
      model.UpdateModel();
    }
  }
}