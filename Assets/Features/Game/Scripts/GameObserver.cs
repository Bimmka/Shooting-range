using Features.GameStates;
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

    private void OnDestroy()
    {
      levelObserver.Cleanup();
    }
  }
}