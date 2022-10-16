using System;

namespace Features.Level.Goal.Scripts
{
  public class GoalObserver
  {
    private readonly int targetsToWin;

    private int currentDestroyedTargets;

    public event Action<int, int> Changed;

    public GoalObserver(int targetsToWin)
    {
      this.targetsToWin = targetsToWin;
    }

    public void IncTargets()
    {
      currentDestroyedTargets++;
      NotifyAboutChange();
    }

    public bool IsWin() => 
      currentDestroyedTargets >= targetsToWin;

    public void Reset()
    {
      currentDestroyedTargets = 0;
      NotifyAboutChange();
    }

    private void NotifyAboutChange() => 
      Changed?.Invoke(currentDestroyedTargets, targetsToWin);
  }
}