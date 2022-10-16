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
    private readonly int targetsOnStart;
    private readonly int targetsToWin;
    private readonly int secondsForGame;
    private readonly GameTimer gameTimer;
    private readonly IGameStateMachine gameStateMachine;

    private int currentDiedTargets;

    public LevelObserver(TargetsContainer targetsContainer, TargetsSpawner spawner, int targetsOnStart, int targetsToWin, int secondsForGame, GameTimer gameTimer, 
      IGameStateMachine gameStateMachine)
    {
      this.targetsContainer = targetsContainer;
      this.targetsContainer.TargetDied += OnTargetDied;
      this.spawner = spawner;
      this.targetsOnStart = targetsOnStart;
      this.targetsToWin = targetsToWin;
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
      currentDiedTargets = 0;
      spawner.RespawnTargets(targetsOnStart);
      gameTimer.Start(secondsForGame);
    }

    private void OnTargetDied()
    {
      currentDiedTargets++;

      if (IsGameWin())
        WinGame();
      else
        SpawnTarget();
    }

    private void SpawnTarget() => 
      spawner.SpawnTarget();

    private bool IsGameWin() => 
      currentDiedTargets >= targetsToWin;

    private void WinGame()
    {
      gameStateMachine.Enter<GameWinState>();
    }

    private void OnTimeOut()
    {
      targetsContainer.DisableTargets();
      gameStateMachine.Enter<GameLooseState>();
    }
  }
}