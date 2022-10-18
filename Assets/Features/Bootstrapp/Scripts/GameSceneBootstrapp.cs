using Features.Game.Scripts;
using Features.GameStates;
using Features.GameStates.States;
using Features.Level.Goal.Scripts;
using Features.Level.Settings;
using Features.Level.Zone.Data;
using Features.Level.Zone.Scripts;
using Features.Services.Coroutine;
using Features.Services.Pause;
using Features.Services.UI.Factory.BaseUI;
using Features.Services.UI.Windows;
using Features.Timer;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp.Scripts
{
  public class GameSceneBootstrapp : MonoInstaller, ICoroutineRunner
  {
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private TargetsZoneBoundsSettings zoneBoundsSettings;
    [SerializeField] private Transform zoneEdgesParent;
    [SerializeField] private GameObserver gameObserver;
    [SerializeField] private UIRoot uiRootPrefab;

    public override void Start()
    {
      base.Start();
      ResolveGameStates();
      Container.InstantiatePrefab(gameObserver);
    }

    public override void InstallBindings()
    {
      BindGoalObserver();
      BindLevelObserver();
      BindGameTimer();
      BindCoroutineRunner();
      BindGameZoneCreator();
      BindGameStateMachine();
      BindWindowService();
      BindUIFactory();
      BindPauseService();
    }

    private void ResolveGameStates()
    {
      Container.Resolve<GameLoadState>();
      Container.Resolve<GameLoopState>();
      Container.Resolve<GameLoseState>();
      Container.Resolve<GameMainMenuState>();
      Container.Resolve<GameRestartState>();
      Container.Resolve<GameWinState>();
    }

    private void BindGoalObserver() => 
      Container.Bind<GoalObserver>().ToSelf().FromNew().AsSingle().WithArguments(levelSettings.TargetsToWin);

    private void BindLevelObserver() =>
      Container.Bind<LevelObserver>().ToSelf().FromNew().AsSingle().WithArguments(levelSettings.TargetsOnStart);

    private void BindGameTimer() => 
      Container.Bind<GameTimer>().ToSelf().FromNew().AsSingle();

    private void BindCoroutineRunner() => 
      Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

    private void BindGameZoneCreator() => 
      Container.Bind<GameZoneCreator>().ToSelf().FromNew().AsSingle().WithArguments(zoneBoundsSettings, zoneEdgesParent);

    private void BindGameStateMachine()
    {
      Container.Bind<IGameStateMachine>().To<GameStateMachine>().FromNew().AsSingle();
      Container.Bind<GameLoadState>().ToSelf().FromNew().AsSingle();
      Container.Bind<GameLoopState>().ToSelf().FromNew().AsSingle().WithArguments(levelSettings);
      Container.Bind<GameLoseState>().ToSelf().FromNew().AsSingle();
      Container.Bind<GameMainMenuState>().ToSelf().FromNew().AsSingle();
      Container.Bind<GameRestartState>().ToSelf().FromNew().AsSingle();
      Container.Bind<GameWinState>().ToSelf().FromNew().AsSingle();
    }

    private void BindWindowService() => 
      Container.Bind<IWindowsService>().To<WindowsService>().FromNew().AsSingle();

    private void BindUIFactory() => 
      Container.Bind<IUIFactory>().To<UIFactory>().FromNew().AsSingle().WithArguments(uiRootPrefab);

    private void BindPauseService() => 
      Container.Bind<IPauseService>().To<PauseService>().FromNew().AsSingle();
  }
}