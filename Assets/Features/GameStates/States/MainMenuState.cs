using Features.GameStates.States.Interfaces;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;

namespace Features.GameStates.States
{
  public class MainMenuState : IState
  {
    private readonly IWindowsService windowsService;

    public MainMenuState(IWindowsService windowsService)
    {
      
      this.windowsService = windowsService;
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