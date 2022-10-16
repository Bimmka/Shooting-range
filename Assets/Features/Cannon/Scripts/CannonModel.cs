using Features.Input;

namespace Features.Cannon.Scripts
{
  public class CannonModel
  {
    private readonly GameZoneRaycaster gameZoneRaycaster;
    private readonly AimDisplayer aimDisplayer;
    private readonly AimFollow aimFollow;
    private readonly IPlayerInputService playerInputService;
    private readonly CannonShooter shooter;

    public CannonModel(GameZoneRaycaster gameZoneRaycaster, AimDisplayer aimDisplayer, AimFollow aimFollow, IPlayerInputService playerInputService, CannonShooter shooter)
    {
      this.gameZoneRaycaster = gameZoneRaycaster;
      this.aimDisplayer = aimDisplayer;
      this.aimFollow = aimFollow;
      this.playerInputService = playerInputService;
      this.shooter = shooter;
    }

    public void UpdateModel()
    {
      gameZoneRaycaster.Raycast(playerInputService.MousePosition);
      aimDisplayer.DisplayAimAt(gameZoneRaycaster.GameZonePosition);
      aimFollow.ChangeRotation(gameZoneRaycaster.GameZonePosition);

      if (playerInputService.IsFire && gameZoneRaycaster.IsHitZone())
        shooter.Shoot(gameZoneRaycaster.GameZonePosition);
    }
  }
}