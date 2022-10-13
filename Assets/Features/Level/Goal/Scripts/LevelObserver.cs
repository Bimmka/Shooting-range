using Features.Targets.Scripts.Base;

namespace Features.Level.Goal.Scripts
{
  public class LevelObserver
  {
    private readonly TargetsContainer targetsContainer;
    private readonly TargetsSpawner spawner;
    private readonly int targetsOnStart;
    private readonly int targetsToWin;

    private int currentDiedTargets;

    public LevelObserver(TargetsContainer targetsContainer, TargetsSpawner spawner, int targetsOnStart, int targetsToWin)
    {
      this.targetsContainer = targetsContainer;
      this.targetsContainer.TargetDied += OnTargetDied;
      this.spawner = spawner;
      this.targetsOnStart = targetsOnStart;
      this.targetsToWin = targetsToWin;
    }

    public void Cleanup() => 
      targetsContainer.TargetDied -= OnTargetDied;

    public void StartLevel() => 
      spawner.SpawnTargets(targetsOnStart);

    private void OnTargetDied()
    {
      currentDiedTargets++;

      if (IsGameWin())
        FinishGame();
      else
        SpawnTarget();
    }

    private void SpawnTarget() => 
      spawner.SpawnTarget();

    private bool IsGameWin() => 
      currentDiedTargets >= targetsToWin;

    private void FinishGame()
    {
      
    }
  }
}