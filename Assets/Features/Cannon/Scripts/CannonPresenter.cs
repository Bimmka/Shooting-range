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
    [SerializeField] private AimFollowSettings aimFollowSettings;
    [SerializeField] private GameZoneRaycasterSettings gameZoneRaycasterSettings;

    private CannonModel model;

    [Inject]
    public void Construct([Inject(Id = "Main Camera")] Camera mainCamera, IPlayerInputService playerInputService, AimView aimView)
    {
      AimDisplayer aimDisplayer = new AimDisplayer(aimView);
      AimFollow aimFollow = new AimFollow(upDownRotation, leftRightRotation, aimFollowSettings);
      GameZoneRaycaster gameZoneRaycaster = new GameZoneRaycaster(gameZoneRaycasterSettings, mainCamera);
      model = new CannonModel(gameZoneRaycaster, aimDisplayer, aimFollow, playerInputService);
    }

    private void Update()
    {
      model.UpdateModel();
    }
  }
}