using Features.GameStates;
using Features.GameStates.States;
using Features.Level.Goal.Scripts;
using UnityEngine;
using Zenject;

namespace Features.Game.Scripts
{
  public class GameObserver : MonoBehaviour
  {
    private IGameStateMachine gameStateMachine;
    private LevelObserver levelObserver;

    [Inject]
    public void Construct(LevelObserver levelObserver, IGameStateMachine gameStateMachine)
    {
      this.levelObserver = levelObserver;
      this.gameStateMachine = gameStateMachine;
    }

    public void StartGame() => 
      gameStateMachine.Enter<GameMainMenuState>();

    private void OnDestroy()
    {
      levelObserver.Cleanup();
    }
  }
}