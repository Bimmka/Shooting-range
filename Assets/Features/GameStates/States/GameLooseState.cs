using Features.GameStates.States.Interfaces;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;

namespace Features.GameStates.States
{
  public class GameLooseState : IState
  {
    private readonly IWindowsService windowsService;

    public GameLooseState(IWindowsService windowsService)
    {
      this.windowsService = windowsService;
    }
    
    public void Enter()
    {
      windowsService.Open(WindowId.Lose);
    }

    public void Exit()
    {
      
    }
  }
}