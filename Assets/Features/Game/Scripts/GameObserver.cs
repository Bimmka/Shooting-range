using Features.GameStates;
using Features.GameStates.States;
using Features.Level.Goal.Scripts;
using Features.Services.UI.Windows;
using UnityEngine;
using Zenject;

namespace Features.Game.Scripts
{
  public class GameObserver : MonoBehaviour
  {
    private IGameStateMachine gameStateMachine;
    private LevelObserver levelObserver;
    private IWindowsService windowsService;

    [Inject]
    public void Construct(LevelObserver levelObserver, IGameStateMachine gameStateMachine, IWindowsService windowsService)
    {
      this.windowsService = windowsService;
      this.levelObserver = levelObserver;
      this.gameStateMachine = gameStateMachine;
    }

    private void Start()
    {
      DontDestroyOnLoad(this);
      gameStateMachine.Enter<GameMainMenuState>();
    }

    private void OnDestroy()
    {
      levelObserver.Cleanup();
      windowsService.Cleanup();
    }
  }
}