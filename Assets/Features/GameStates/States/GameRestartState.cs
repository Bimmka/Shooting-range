using Features.GameStates.States.Interfaces;
using Features.Level.Goal.Scripts;

namespace Features.GameStates.States
{
  public class GameRestartState : IState
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly LevelObserver levelObserver;

    public GameRestartState(IGameStateMachine gameStateMachine, LevelObserver levelObserver)
    {
      this.gameStateMachine = gameStateMachine;
      this.levelObserver = levelObserver;
      gameStateMachine.Register(this);
    }
    
    public void Enter()
    {
      levelObserver.RestartLevel();
      gameStateMachine.Enter<GameLoopState>();
    }

    public void Exit()
    {
      
    }
  }
}