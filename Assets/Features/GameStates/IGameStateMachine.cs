using Features.GameStates.States.Interfaces;
using Features.Services;

namespace Features.GameStates
{
  public interface IGameStateMachine : IService
  {
    void Enter<TState>() where TState : class, IState;
    void Register<TState>(TState state) where TState : class, IExitableState;
  }
}