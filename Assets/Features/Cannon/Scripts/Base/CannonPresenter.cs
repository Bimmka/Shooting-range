using System;
using System.Collections;
using Features.Bullet.Scripts;
using Features.Cannon.Data;
using Features.Cannon.Scripts.Aim;
using Features.Cannon.Scripts.Effects;
using Features.Input;
using Features.Services.Pause;
using UnityEngine;
using Zenject;

namespace Features.Cannon.Scripts.Base
{
  public class CannonPresenter : MonoBehaviour, IPaused
  {
    [SerializeField] private Transform upDownRotation;
    [SerializeField] private Transform leftRightRotation;
    [SerializeField] private Transform bulletStartPosition;
    [SerializeField] private AimFollowSettings aimFollowSettings;
    [SerializeField] private GameZoneRaycasterSettings gameZoneRaycasterSettings;
    [SerializeField] private CannonShooterSettings shooterSettings;
    [SerializeField] private ParticleSystem particleSystem;

    private CannonModel model;
    private IPauseService pauseService;

    private bool isLocked;

    [Inject]
    public void Construct([Inject(Id = "Main Camera")] Camera mainCamera, IPlayerInputService playerInputService, AimView aimView, BulletsContainer bulletsContainer, 
      IPauseService pauseService)
    {
      this.pauseService = pauseService;
      AimDisplayer aimDisplayer = new AimDisplayer(aimView);
      AimFollow aimFollow = new AimFollow(upDownRotation, leftRightRotation, aimFollowSettings);
      GameZoneRaycaster gameZoneRaycaster = new GameZoneRaycaster(gameZoneRaycasterSettings, mainCamera);
      ShootEffectPlayer shootEffectPlayer = new ShootEffectPlayer(particleSystem);
      CannonShooter cannonShooter = new CannonShooter(bulletsContainer, bulletStartPosition, shooterSettings, shootEffectPlayer);
      model = new CannonModel(gameZoneRaycaster, aimDisplayer, aimFollow, playerInputService, cannonShooter);
      
      pauseService.Register(this);
    }

    private void OnDestroy()
    {
      pauseService.Unregister(this);
    }

    public void StopTick() => 
      isLocked = true;

    public void StartTick()
    {
      isLocked = false;
      StartCoroutine(Tick());
    }

    public void Pause() => 
      StopTick();

    public void Unpause() => 
      StartTick();

    private IEnumerator Tick()
    {
      while (isLocked == false)
      {
        model.UpdateModel();
        yield return null;
      }
    }
  }
}