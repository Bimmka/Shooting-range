using Features.Services.Pause;
using Features.UI.Windows.Base.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Windows.Window.PauseMenu.Scripts
{
  public class UIPauseMenu : BaseWindow
  {
    [SerializeField] private Button continueButton;
    [SerializeField] private Button closeGameButton;
    private IPauseService pauseService;

    public void Construct(IPauseService pauseService)
    {
      this.pauseService = pauseService;
    }

    public override void Open()
    {
      pauseService.Pause();
      base.Open();
    }

    public override void Destroy()
    {
      pauseService.Unpause();
      base.Destroy();
    }

    protected override void Subscribe()
    {
      base.Subscribe();
      continueButton.onClick.AddListener(ContinueGame);
      closeGameButton.onClick.AddListener(QuitGame);
    }

    protected override void Cleanup()
    {
      base.Cleanup();
      continueButton.onClick.RemoveListener(ContinueGame);
      closeGameButton.onClick.RemoveListener(QuitGame);
    }

    private void ContinueGame() => 
      Destroy();

    private void QuitGame() => 
      Application.Quit();
  }
}