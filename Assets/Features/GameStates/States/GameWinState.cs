using Features.GameStates.States.Interfaces;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Targets.Scripts.Base;
using Features.Timer;

namespace Features.GameStates.States
{
  public class GameWinState : IState
  {
    private readonly IWindowsService windowsService;
    public GameWinState(IGameStateMachine gameStateMachine, IWindowsService windowsService)
    {
      this.windowsService = windowsService;
      gameStateMachine.Register(this);
    }
    
    public void Enter()
    {
      windowsService.Open(WindowId.Win);
    }

    public void Exit()
    {
      
    }
  }
}