using System.Threading;
using Features.Cannon.Scripts;
using Features.GameStates.States.Interfaces;

namespace Features.GameStates.States
{
  public class GameLoopState : IState
  {
    private readonly CannonPresenter cannonPresenter;

    public GameLoopState(IGameStateMachine gameStateMachine, CannonPresenter cannonPresenter)
    {
      this.cannonPresenter = cannonPresenter;
      gameStateMachine.Register(this);
    }
    public void Enter() => 
      cannonPresenter.StartTick();

    public void Exit() => 
      cannonPresenter.StopTick();
  }
}