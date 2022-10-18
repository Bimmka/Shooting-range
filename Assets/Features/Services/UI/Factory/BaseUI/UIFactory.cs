using System;
using Features.GameStates;
using Features.Level.Goal.Scripts;
using Features.Services.Assets;
using Features.Services.Pause;
using Features.Services.StaticData;
using Features.Services.UI.Windows;
using Features.Timer;
using Features.UI.Data;
using Features.UI.Windows.Base.Scripts;
using Features.UI.Windows.Window.HUD.Scripts;
using Features.UI.Windows.Window.LoseWindow.Scripts;
using Features.UI.Windows.Window.MainMenu.Scripts;
using Features.UI.Windows.Window.PauseMenu.Scripts;
using Features.UI.Windows.Window.WinWindow.Scripts;
using UnityEngine;
using Zenject;

namespace Features.Services.UI.Factory.BaseUI
{
  public class UIFactory : IUIFactory
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly IAssetProvider assets;
    private readonly IStaticDataService staticData;
    private readonly UIRoot uiRootPrefab;
    private readonly Camera mainCamera;
    private readonly GoalObserver goalObserver;
    private readonly GameTimer gameTimer;
    private readonly IPauseService pauseService;

    private Transform uiRoot;

    public event Action<WindowId,BaseWindow> Spawned;

    public UIFactory(IGameStateMachine gameStateMachine, IAssetProvider assets, IStaticDataService staticData, UIRoot uiRootPrefab, 
      [Inject(Id = "Main Camera")]Camera mainCamera, GoalObserver goalObserver, GameTimer gameTimer, IPauseService pauseService)
    {
      this.gameStateMachine = gameStateMachine;
      this.assets = assets;
      this.staticData = staticData;
      this.uiRootPrefab = uiRootPrefab;
      this.mainCamera = mainCamera;
      this.goalObserver = goalObserver;
      this.gameTimer = gameTimer;
      this.pauseService = pauseService;
    }


    public void CreateWindow(WindowId id, IWindowsService windowsService)
    {
      WindowInstantiateData config = LoadWindowInstantiateData(id);
      
      if (uiRoot == null)
        CreateUIRoot();
      
      switch (id)
      {
        case WindowId.MainMenu:
          CreateMainMenu(config, gameStateMachine);
          break;
        case WindowId.Pause:
          CreatePauseMenu(config, pauseService);
          break;
        case WindowId.Win:
          CreateWinWindow(config, gameStateMachine);
          break;
        case WindowId.Lose:
          CreateLoseWindow(config,gameStateMachine);
          break;
        case WindowId.HUD:
          CreateHUDWindow(config,windowsService, goalObserver, gameTimer);
          break;
        default:
          CreateWindow(config, id);
          break;
      }
    }

    private void CreateMainMenu(WindowInstantiateData config, IGameStateMachine gameStateMachine)
    {
      BaseWindow window = InstantiateWindow(config, uiRoot);
      ((UIMainMenu)window).Construct(gameStateMachine);
      NotifyAboutCreateWindow(config.ID, window);
    }

    private void CreatePauseMenu(WindowInstantiateData config, IPauseService pauseService)
    {
      BaseWindow window = InstantiateWindow(config, uiRoot);
      ((UIPauseMenu) window).Construct(pauseService);
      NotifyAboutCreateWindow(config.ID, window);
    }

    private void CreateLoseWindow(WindowInstantiateData config, IGameStateMachine gameStateMachine)
    {
      BaseWindow window = InstantiateWindow(config, uiRoot);
      ((UILoseWindow)window).Construct(gameStateMachine);
      NotifyAboutCreateWindow(config.ID, window);
    }
    
    private void CreateHUDWindow(WindowInstantiateData config, IWindowsService windowsService, GoalObserver goalObserver, GameTimer gameTimer)
    {
      BaseWindow window = InstantiateWindow(config, uiRoot);
      ((HUD)window).Construct(windowsService);
      window.GetComponent<GameTimeDisplayer>().Construct(gameTimer);
      window.GetComponent<LevelGoalDisplayer>().Construct(goalObserver);
      NotifyAboutCreateWindow(config.ID, window);
    }
    
    private void CreateWinWindow(WindowInstantiateData config, IGameStateMachine gameStateMachine)
    {
      BaseWindow window = InstantiateWindow(config, uiRoot);
      ((UIWinWindow)window).Construct(gameStateMachine);
      NotifyAboutCreateWindow(config.ID, window);
    }

    private void CreateUIRoot()
    {
        if (uiRoot != null)
            return;

        UIRoot prefab = assets.Instantiate(uiRootPrefab).GetComponent<UIRoot>();

        prefab.SetCamera(mainCamera);
        uiRoot = prefab.transform;
    }

    private void CreateWindow(WindowInstantiateData config, WindowId id)
    {
      BaseWindow window = InstantiateWindow(config);
      NotifyAboutCreateWindow(id, window);
    }

    private BaseWindow InstantiateWindow(WindowInstantiateData config)
    {
      BaseWindow window = assets.Instantiate(config.Window, uiRoot);
      window.SetID(config.ID);
      return window;
    }

    private BaseWindow InstantiateWindow(WindowInstantiateData config, Transform parent)
    {
      BaseWindow window = assets.Instantiate(config.Window, parent);
      window.SetID(config.ID);
      return window;
    }

    private void NotifyAboutCreateWindow(WindowId id, BaseWindow window) => 
      Spawned?.Invoke(id, window);

    private WindowInstantiateData LoadWindowInstantiateData(WindowId id) => 
      staticData.ForWindow(id);
  }
}