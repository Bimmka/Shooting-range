using System;
using Features.Services.Assets;
using Features.Services.Coroutine;
using Features.Services.StaticData;
using Features.Targets.Scripts.Elements;
using Features.Targets.Scripts.HP;
using Features.Targets.Scripts.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Targets.Scripts.Base
{
  public class TargetsFactory
  {
    private readonly IAssetProvider assetProvider;
    private readonly IStaticDataService staticDataService;
    private readonly Transform spawnParent;
    private readonly TargetPresenter prefab;
    private readonly ICoroutineRunner coroutineRunner;

    public TargetsFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, Transform spawnParent,
      TargetPresenter prefab, ICoroutineRunner coroutineRunner)
    {
      this.assetProvider = assetProvider;
      this.staticDataService = staticDataService;
      this.spawnParent = spawnParent;
      this.prefab = prefab;
      this.coroutineRunner = coroutineRunner;
    }

    public TargetPresenter Spawn(TargetType type, Vector3 position)
    {
      TargetSettings settings = staticDataService.Settings(type);
      TargetPresenter presenter = assetProvider.Instantiate(prefab, spawnParent);
      TargetMover mover = new TargetMover(presenter.GetComponent<Rigidbody2D>(),  settings.MoveSpeed, coroutineRunner);
      TargetHP hp = new TargetHP(settings.MaxHp);
      
      presenter.Initialize(settings, Guid.NewGuid().ToString(), mover, hp);
      presenter.GetComponent<TargetHPDisplayer>().Construct(hp);
      presenter.SetPosition(position);
      presenter.SetMoveDirection(MoveDirection());
      presenter.Show();
      return presenter;
    }

    public void RespawnTarget(TargetPresenter presenter, Vector3 position)
    {
      presenter.Restore();
      presenter.SetPosition(position);
      presenter.SetMoveDirection(MoveDirection());
      presenter.Show();
    }

    private Vector2 MoveDirection() => 
      new Vector2(Random.value, Random.value);
  }
}