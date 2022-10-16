using Features.GameStates.States.Interfaces;
using Features.Level.Goal.Scripts;
using Features.Level.Zone.Scripts;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Zenject;

namespace Features.GameStates.States
{
  public class GameLoadState : IState
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly LevelObserver levelObserver;
    private readonly GameZoneCreator gameZoneCreator;
    private readonly IWindowsService windowsService;

    [Inject]
    public GameLoadState(IGameStateMachine gameStateMachine, LevelObserver levelObserver, GameZoneCreator gameZoneCreator, IWindowsService windowsService)
    {
      this.gameStateMachine = gameStateMachine;
      this.levelObserver = levelObserver;
      this.gameZoneCreator = gameZoneCreator;
      this.windowsService = windowsService;
      gameStateMachine.Register(this);
    }

    public void Enter()
    {
      gameZoneCreator.CreateGameZone();
      windowsService.Open(WindowId.HUD);
      levelObserver.StartLevel();
      gameStateMachine.Enter<GameLoopState>();
    }

    public void Exit()
    {
      
    }
  }
}