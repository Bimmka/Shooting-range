using System;
using System.Collections.Generic;
using Features.GameStates.States;
using Features.GameStates.States.Interfaces;

namespace Features.GameStates
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;
    
    public GameStateMachine()
    {
      _states = new Dictionary<Type, IExitableState>(6);
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Register<TState>(TState state) where TState : class, IExitableState => 
      _states.Add(typeof(TState), state);

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}