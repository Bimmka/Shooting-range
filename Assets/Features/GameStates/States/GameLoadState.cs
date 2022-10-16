using Features.GameStates.States.Interfaces;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Zenject;

namespace Features.GameStates.States
{
  public class GameLoadState : IState
  {
    private readonly GameStateMachine gameStateMachine;
    private readonly IWindowsService windowsService;

    [Inject]
    public GameLoadState(GameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
      gameStateMachine.Add(this);
    }

    public void Enter()
    {
      
    }

    public void Exit()
    {
      
    }

    private void OnLoad()
    {
      CreateHUD();
      gameStateMachine.Enter<GameLoopState>();
    }
    
    private void CreateHUD() => 
      windowsService.Open(WindowId.Pause);
    
  }
}