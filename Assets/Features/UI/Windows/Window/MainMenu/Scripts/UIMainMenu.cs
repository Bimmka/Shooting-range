using Features.GameStates;
using Features.GameStates.States;
using Features.UI.Windows.Base;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Windows.Window.MainMenu.Scripts
{
  public class UIMainMenu : BaseWindow
  {
    [SerializeField] private Button playButton;
    private IGameStateMachine gameStateMachine;

    public void Construct(IGameStateMachine gameStateMachine)
    {
      this.gameStateMachine = gameStateMachine;
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      playButton.onClick.AddListener(StartGame);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      playButton.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
      gameStateMachine.Enter<GameLoadState>();
      Destroy();
    }
  }
}