using Features.Level.Goal.Scripts;
using TMPro;
using UnityEngine;

namespace Features.UI.Windows.Window.HUD.Scripts
{
  public class LevelGoalDisplayer : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI goalText;
    private GoalObserver goalObserver;

    public void Construct(GoalObserver goalObserver)
    {
      this.goalObserver = goalObserver;
      goalObserver.Changed += Display;
    }

    public void Cleanup()
    {
      goalObserver.Changed -= Display;
    }

    private void Display(int currentCount, int maxCount) => 
      goalText.text = $"{currentCount} / {maxCount}";
  }
}