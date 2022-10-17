using Features.GameStates;
using Features.GameStates.States;
using Features.Targets.Scripts.Base;
using Features.Timer;

namespace Features.Level.Goal.Scripts
{
  public class LevelObserver
  {
    private readonly TargetsContainer targetsContainer;
    private readonly TargetsSpawner spawner;
    private readonly GoalObserver goalObserver;
    private readonly GameTimer gameTimer;
    private readonly IGameStateMachine gameStateMachine;
    private readonly int targetsOnStart;
    public LevelObserver(TargetsContainer targetsContainer, TargetsSpawner spawner, GoalObserver goalObserver, int targetsOnStart, GameTimer gameTimer, 
      IGameStateMachine gameStateMachine)
    {
      this.targetsContainer = targetsContainer;
      this.targetsContainer.TargetDied += OnTargetDied;
      this.spawner = spawner;
      this.goalObserver = goalObserver;
      this.targetsOnStart = targetsOnStart;
      this.gameTimer = gameTimer;
      this.gameStateMachine = gameStateMachine;
      this.gameTimer.TimeOut += OnTimeOut;
    }

    public void Cleanup()
    {
      targetsContainer.TargetDied -= OnTargetDied;
      gameTimer.TimeOut -= OnTimeOut;
      targetsContainer.Cleanup();
      spawner.Cleanup();
    }

    public void StartLevel()
    {
      goalObserver.Refresh();
      spawner.SpawnTargets(targetsOnStart);
    }

    public void RestartLevel()
    {
      goalObserver.Reset();
      spawner.RespawnTargets(targetsOnStart);
    }

    private void OnTargetDied()
    {
      goalObserver.IncTargets();

      if (IsGameWin())
        WinGame();
      else
        SpawnTarget();
    }

    private void SpawnTarget() => 
      spawner.SpawnTarget();

    private bool IsGameWin() =>
      goalObserver.IsWin();

    private void WinGame() => 
      gameStateMachine.Enter<GameWinState>();

    private void OnTimeOut() => 
      gameStateMachine.Enter<GameLoseState>();
  }
}