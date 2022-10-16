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
    private readonly int secondsForGame;
    public LevelObserver(TargetsContainer targetsContainer, TargetsSpawner spawner, GoalObserver goalObserver, int targetsOnStart,  int secondsForGame, GameTimer gameTimer, 
      IGameStateMachine gameStateMachine)
    {
      this.targetsContainer = targetsContainer;
      this.targetsContainer.TargetDied += OnTargetDied;
      this.spawner = spawner;
      this.goalObserver = goalObserver;
      this.targetsOnStart = targetsOnStart;
      this.secondsForGame = secondsForGame;
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
      spawner.SpawnTargets(targetsOnStart);
      gameTimer.Start(secondsForGame);
    }

    public void RestartLevel()
    {
      goalObserver.Reset();
      spawner.RespawnTargets(targetsOnStart);
      gameTimer.Start(secondsForGame);
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

    private void WinGame()
    {
      gameStateMachine.Enter<GameWinState>();
    }

    private void OnTimeOut()
    {
      targetsContainer.DisableTargets();
      gameStateMachine.Enter<GameLoseState>();
    }
  }
}