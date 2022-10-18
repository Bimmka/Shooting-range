using System.Threading;
using Features.Cannon.Scripts;
using Features.Cannon.Scripts.Base;
using Features.GameStates.States.Interfaces;
using Features.Level.Settings;
using Features.Targets.Scripts.Base;
using Features.Timer;

namespace Features.GameStates.States
{
  public class GameLoopState : IState
  {
    private readonly CannonPresenter cannonPresenter;
    private readonly GameTimer gameTimer;
    private readonly TargetsContainer targetsContainer;
    private readonly LevelSettings levelSettings;

    public GameLoopState(IGameStateMachine gameStateMachine, CannonPresenter cannonPresenter, GameTimer gameTimer, TargetsContainer targetsContainer, LevelSettings levelSettings)
    {
      this.cannonPresenter = cannonPresenter;
      this.gameTimer = gameTimer;
      this.targetsContainer = targetsContainer;
      this.levelSettings = levelSettings;
      gameStateMachine.Register(this);
    }
    public void Enter()
    {
      gameTimer.Start(levelSettings.GameSecondsTime);
      cannonPresenter.StartTick();
    }

    public void Exit()
    {
      cannonPresenter.StopTick();
      targetsContainer.DisableTargets();
      gameTimer.Stop();
    }
  }
}