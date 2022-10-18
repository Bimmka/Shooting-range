using Features.Services.Assets;
using Features.Services.Coroutine;
using Features.Services.Pause;
using Features.Services.StaticData;
using Features.Targets.Scripts.Elements;
using Features.Targets.Scripts.HP;
using Features.Targets.Scripts.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Targets.Scripts.Spawn
{
  public class TargetsFactory
  {
    private readonly IAssetProvider assetProvider;
    private readonly IStaticDataService staticDataService;
    private readonly Transform spawnParent;
    private readonly TargetPresenter prefab;
    private readonly ICoroutineRunner coroutineRunner;
    private readonly IPauseService pauseService;

    public TargetsFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, Transform spawnParent,
      TargetPresenter prefab, ICoroutineRunner coroutineRunner, IPauseService pauseService)
    {
      this.assetProvider = assetProvider;
      this.staticDataService = staticDataService;
      this.spawnParent = spawnParent;
      this.prefab = prefab;
      this.coroutineRunner = coroutineRunner;
      this.pauseService = pauseService;
    }

    public TargetPresenter Spawn(TargetType type, Vector3 position)
    {
      TargetSettings settings = staticDataService.Settings(type);
      TargetPresenter presenter = assetProvider.Instantiate(prefab, spawnParent);
      TargetMover mover = new TargetMover(presenter.GetComponent<Rigidbody2D>(),  settings.MoveSpeed, coroutineRunner);
      TargetHP hp = new TargetHP(settings.MaxHp);

      InitializeView(presenter, settings);
      ConstructPresenter(presenter, pauseService);
      InitializePresenter(position, presenter, settings, mover, hp);
      return presenter;
    }

    public void RespawnTarget(TargetPresenter presenter, Vector3 position)
    {
      presenter.Restore();
      presenter.SetPosition(position);
      presenter.SetMoveDirection(MoveDirection());
      presenter.Show();
    }

    private void ConstructPresenter(TargetPresenter presenter, IPauseService pauseService) => 
      presenter.Construct(pauseService);

    private void InitializePresenter(Vector3 position, TargetPresenter presenter, TargetSettings settings,
      TargetMover mover, TargetHP hp)
    {
      presenter.Initialize(settings, mover, hp);
      presenter.GetComponent<TargetHPDisplayer>().Construct(hp);
      presenter.SetPosition(position);
      presenter.SetMoveDirection(MoveDirection());
      presenter.Show();
    }

    private void InitializeView(TargetPresenter presenter, TargetSettings settings)
    {
      TargetViewMarker viewMarker = presenter.GetComponentInChildren<TargetViewMarker>();
      TargetView view = presenter.GetComponent<TargetView>();

      view.Initialize(assetProvider.Instantiate(settings.View, viewMarker.transform));
    }

    private Vector2 MoveDirection() => 
      new Vector2(Random.value, Random.value);
  }
}