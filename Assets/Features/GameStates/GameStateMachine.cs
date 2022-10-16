using System;
using System.Collections.Generic;
using Features.Cannon.Scripts;
using Features.GameStates.States;
using Features.GameStates.States.Interfaces;
using Features.Level.Goal.Scripts;
using Features.Level.Zone.Scripts;
using Features.Services.UI.Windows;
using Zenject;

namespace Features.GameStates
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    [Inject]
    public GameStateMachine(LevelObserver levelObserver, CannonPresenter cannonPresenter, IWindowsService windowsService, GameZoneCreator gameZoneCreator)
    {
      _states = new Dictionary<Type, IExitableState>()
      {
        [typeof(GameLoadState)] = new GameLoadState(this, levelObserver, gameZoneCreator, windowsService),
        [typeof(GameLoopState)] = new GameLoopState(cannonPresenter),
        [typeof(GameLooseState)] = new GameLooseState(windowsService),
        [typeof(GameMainMenuState)] = new GameMainMenuState( windowsService),
        [typeof(GameRestartState)] = new GameRestartState(this, levelObserver),
        [typeof(GameWinState)] = new GameWinState(windowsService),
      };
  
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;


    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }
  }
}