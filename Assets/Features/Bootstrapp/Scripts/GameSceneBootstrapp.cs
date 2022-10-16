using Features.Cannon.Data;
using Features.Cannon.Scripts;
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
    [SerializeField] private Transform cannonSpawnParent;
    [SerializeField] private TargetPresenter targetPrefab;
    [SerializeField] private CannonPresenter cannonPrefab;
    [SerializeField] private AimView aimViewPrefab;

    public override void Start()
    {
      base.Start();
      
      Container.InstantiatePrefab(cannonPrefab, cannonSpawnParent);

      Container.Resolve<GameZoneCreator>().CreateGameZone();
      Container.Resolve<LevelObserver>().StartLevel();
    }

    public override void InstallBindings()
    {
      IStaticDataService staticDataService = new StaticDataService();
      staticDataService.Load();
      Container.Bind<IStaticDataService>().FromInstance(staticDataService).AsSingle();
      Container.Bind<IAssetProvider>().To<AssetProvider>().FromNew().AsSingle();
      Container.Bind<TargetsContainer>().ToSelf().FromNew().AsSingle();
      Container.Bind<TargetsFactory>().ToSelf().FromNew().AsSingle().WithArguments(targetSpawnParent, targetPrefab);
      Container.Bind<TargetsSpawner>().ToSelf().FromNew().AsSingle();
      Container.Bind<LevelObserver>().ToSelf().FromNew().AsSingle().WithArguments(levelSettings.TargetsOnStart, levelSettings.TargetsToWin);
      Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
      Container.Bind<GameZoneCreator>().ToSelf().FromNew().AsSingle().WithArguments(zoneBoundsSettings, zoneEdgesParent);
      Container.Bind<IPlayerInputService>().To<PlayerInputService>().FromNew().AsSingle();
      Container.Bind<Camera>().WithId("Main Camera").FromInstance(Camera.main).AsSingle();
      Container.Bind<AimView>().ToSelf().FromComponentInNewPrefab(aimViewPrefab).AsSingle();
    }
  }
}