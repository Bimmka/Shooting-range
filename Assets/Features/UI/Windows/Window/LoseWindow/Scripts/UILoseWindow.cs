using Features.GameStates;
using Features.GameStates.States;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Windows.Window.LoseWindow.Scripts
{
  public class UILoseWindow : BaseWindow
  {
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitGameButton;
    
    private IGameStateMachine gameStateMachine;

    public void Construct(IGameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
    }
    
    protected override void Subscribe()
    {
      base.Subscribe();
      restartButton.onClick.AddListener(RestartGame);
      quitGameButton.onClick.AddListener(QuitGame);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      restartButton.onClick.RemoveListener(RestartGame);
      quitGameButton.onClick.RemoveListener(QuitGame);
    }

    private void RestartGame()
    {
      Destroy();
      gameStateMachine.Enter<GameRestartState>();
    }

    private void QuitGame()
    {
      Application.Quit();
    }
  }
}