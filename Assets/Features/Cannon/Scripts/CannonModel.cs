using Features.Input;

namespace Features.Cannon.Scripts
{
  public class CannonModel
  {
    private readonly GameZoneRaycaster gameZoneRaycaster;
    private readonly AimDisplayer aimDisplayer;
    private readonly AimFollow aimFollow;
    private readonly IPlayerInputService playerInputService;

    public CannonModel(GameZoneRaycaster gameZoneRaycaster, AimDisplayer aimDisplayer, AimFollow aimFollow, IPlayerInputService playerInputService)
    {
      this.gameZoneRaycaster = gameZoneRaycaster;
      this.aimDisplayer = aimDisplayer;
      this.aimFollow = aimFollow;
      this.playerInputService = playerInputService;
    }

    public void UpdateModel()
    {
      gameZoneRaycaster.Raycast(playerInputService.MousePosition);
      aimDisplayer.DisplayAim(gameZoneRaycaster.ViewPortGameZonePosition);
      aimFollow.ChangeRotation(gameZoneRaycaster.ViewPortGameZonePosition);
    }
  }
}