using Features.GameStates.States.Interfaces;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;

namespace Features.GameStates.States
{
  public class GameWinState : IState
  {
    private readonly IWindowsService windowsService;

    public GameWinState(IWindowsService windowsService)
    {
      this.windowsService = windowsService;
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