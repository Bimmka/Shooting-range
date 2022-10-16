using Features.GameStates.States.Interfaces;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;

namespace Features.GameStates.States
{
  public class GameMainMenuState : IState
  {
    private readonly IWindowsService windowsService;

    public GameMainMenuState(IGameStateMachine gameStateMachine, IWindowsService windowsService)
    {
      this.windowsService = windowsService;
      gameStateMachine.Register(this);
    }
    
    public void Enter()
    {
      windowsService.Open(WindowId.MainMenu);  
    }

    public void Exit()
    {
      
    }
  }
}