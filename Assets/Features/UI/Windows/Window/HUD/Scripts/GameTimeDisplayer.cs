using Features.Timer;
using TMPro;
using UnityEngine;

namespace Features.UI.Windows.Window.HUD.Scripts
{
  public class GameTimeDisplayer : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI timeText;
    private GameTimer gameTimer;

    public void Construct(GameTimer gameTimer)
    {
      this.gameTimer = gameTimer;
      Display(gameTimer.LeftSeconds);
      gameTimer.Changed += Display;
    }

    public void Cleanup()
    {
      gameTimer.Changed -= Display;
    }

    private void Display(int leftSeconds)
    {
      timeText.text = leftSeconds.ToString();
    }
  }
}