using Features.Level.Goal.Scripts;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Timer;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Windows.Window.HUD.Scripts
{
  [RequireComponent(typeof(GameTimeDisplayer))]
  [RequireComponent(typeof(LevelGoalDisplayer))]
  public class HUD : BaseWindow
  {
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameTimeDisplayer gameTimeDisplayer;
    [SerializeField] private LevelGoalDisplayer goalDisplayer;
    
    private IWindowsService windowsService;

    public void Construct(IWindowsService windowsService)
    {
      this.windowsService = windowsService;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      pauseButton.onClick.AddListener(OpenPauseMenu);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      gameTimeDisplayer.Cleanup();
      goalDisplayer.Cleanup();
    }

    private void OpenPauseMenu() => 
      windowsService.Open(WindowId.Pause);
  }
}