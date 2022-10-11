using System;
using Features.Services.Assets;
using Features.Services.StaticData;
using Features.Targets.Scripts.Elements;
using Features.Targets.Scripts.HP;
using Features.Targets.Scripts.Settings;
using UnityEngine;

namespace Features.Targets.Scripts.Base
{
  public class TargetsFactory
  {
    private readonly TargetsContainer targetsContainer;
    private readonly IAssetProvider assetProvider;
    private readonly IStaticDataService staticDataService;
    private readonly Transform spawnParent;
    private readonly TargetPresenter prefab;

    public TargetsFactory(TargetsContainer targetsContainer, IAssetProvider assetProvider, IStaticDataService staticDataService, Transform spawnParent,
      TargetPresenter prefab)
    {
      this.targetsContainer = targetsContainer;
      this.assetProvider = assetProvider;
      this.staticDataService = staticDataService;
      this.spawnParent = spawnParent;
      this.prefab = prefab;
    }

    public TargetPresenter Spawn(TargetType type, Vector3 position)
    {
      TargetSettings settings = staticDataService.Settings(type);
      TargetPresenter presenter = assetProvider.Instantiate(prefab, spawnParent);
      TargetMover mover = new TargetMover(settings.MoveSpeed);
      TargetHP hp = new TargetHP(settings.MaxHp);
      presenter.Initialize(settings, Guid.NewGuid().ToString(), mover, hp);
      presenter.GetComponent<TargetHPDisplayer>().Construct(hp);

      return presenter;
    }
  }
}