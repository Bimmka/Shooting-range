using Features.Bullet.Scripts;
using Features.Cannon.Scripts;
using Features.Game;
using Features.Game.Scripts;
using Features.GameStates;
using Features.Input;
using Features.Level.Goal.Scripts;
using Features.Level.Settings;
using Features.Level.Zone.Data;
using Features.Level.Zone.Scripts;
using Features.Services.Assets;
using Features.Services.Coroutine;
using Features.Services.StaticData;
using Features.Targets.Scripts.Base;
using Features.Targets.Scripts.Elements;
using Features.Timer;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp.Scripts
{
  public class GameSceneBootstrapp : MonoInstaller, ICoroutineRunner
  {
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private TargetsZoneBoundsSettings zoneBoundsSettings;
    [SerializeField] private Transform zoneEdgesParent;
    [SerializeField] private Transform targetSpawnParent;
    [SerializeField] private TargetPresenter targetPrefab;
    [SerializeField] private CannonPresenter cannonPrefab;
    [SerializeField] private AimView aimViewPrefab;
    [SerializeField] private BulletPresenter bulletPrefab;
    [SerializeField] private Transform bulletSpawnParent;
    [SerializeField] private GameObserver gameObserver;

    public override void Start()
    {
      base.Start();
      Container.InstantiatePrefab(gameObserver);
    }

    public override void InstallBindings()
    {
      BindStaticData();
      BindAssetProvider();
      BindTargetsContainer();
      BindTargetsFactory();
      BindTargetsSpawner();
      BindLevelObserver();
      BindGameTimer();
      BindCoroutineRunner();
      BindGameZoneCreator();
      BindPlayerInput();
      BindMainCamera();
      BindAimView();
      BindBulletsContainer();
      BindBulletFactory();
      BindCannonPresenter();
      BindGameStateMachine();
    }

    private void BindStaticData()
    {
      IStaticDataService staticDataService = new StaticDataService();
      staticDataService.Load();
      Container.Bind<IStaticDataService>().FromInstance(staticDataService).AsSingle();
    }

    private void BindAssetProvider() => 
      Container.Bind<IAssetProvider>().To<AssetProvider>().FromNew().AsSingle();

    private void BindTargetsContainer() => 
      Container.Bind<TargetsContainer>().ToSelf().FromNew().AsSingle();

    private void BindTargetsFactory() => 
      Container.Bind<TargetsFactory>().ToSelf().FromNew().AsSingle().WithArguments(targetSpawnParent, targetPrefab);

    private void BindTargetsSpawner() => 
      Container.Bind<TargetsSpawner>().ToSelf().FromNew().AsSingle();

    private void BindLevelObserver() =>
      Container.Bind<LevelObserver>().ToSelf().FromNew().AsSingle().WithArguments(levelSettings.TargetsOnStart,
        levelSettings.TargetsToWin, levelSettings.GameSecondsTime);

    private void BindGameTimer() => 
      Container.Bind<GameTimer>().ToSelf().FromNew().AsSingle();

    private void BindCoroutineRunner() => 
      Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

    private void BindGameZoneCreator() => 
      Container.Bind<GameZoneCreator>().ToSelf().FromNew().AsSingle().WithArguments(zoneBoundsSettings, zoneEdgesParent);

    private void BindPlayerInput() => 
      Container.Bind<IPlayerInputService>().To<PlayerInputService>().FromNew().AsSingle();

    private void BindMainCamera() => 
      Container.Bind<Camera>().WithId("Main Camera").FromInstance(Camera.main).AsSingle();

    private void BindAimView() => 
      Container.Bind<AimView>().ToSelf().FromComponentInNewPrefab(aimViewPrefab).AsSingle();

    private void BindBulletsContainer() => 
      Container.Bind<BulletsContainer>().ToSelf().FromNew().AsSingle();

    private void BindBulletFactory() => 
      Container.Bind<BulletFactory>().ToSelf().FromNew().AsSingle().WithArguments(bulletPrefab, bulletSpawnParent);

    private void BindCannonPresenter() => 
      Container.Bind<CannonPresenter>().ToSelf().FromComponentInNewPrefab(cannonPrefab).AsSingle();

    private void BindGameStateMachine() => 
      Container.Bind<IGameStateMachine>().To<GameStateMachine>().FromNew().AsSingle();
  }
}