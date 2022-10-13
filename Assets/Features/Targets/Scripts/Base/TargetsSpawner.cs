using System;
using Features.Targets.Scripts.Elements;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Targets.Scripts.Base
{
  public class TargetsSpawner
  {
    private readonly TargetsContainer targetsContainer;
    private readonly TargetsFactory factory;

    private readonly int targetTypeCount;

    public TargetsSpawner(TargetsContainer targetsContainer, TargetsFactory factory)
    {
      this.targetsContainer = targetsContainer;
      this.factory = factory;

      targetTypeCount = Enum.GetNames(typeof(TargetType)).Length;
    }

    public void Cleanup()
    {
      targetsContainer.Cleanup();
    }

    public void SpawnTargets(int count)
    {
      TargetPresenter presenter;
      for (int i = 0; i < count; i++)
      {
        presenter = factory.Spawn(RandomType(), Vector3.zero);
        targetsContainer.AddTarget(presenter);
      }
    }

    public void SpawnTarget()
    {
      TargetType type = RandomType();
      if (targetsContainer.IsContainsDisabledPresenter(type))
        factory.RespawnTarget(targetsContainer.PresenterOrNull(type), Vector3.right * 5);
      else
        factory.Spawn(type, Vector3.right * 5);
    }

    private TargetType RandomType() => 
      (TargetType)Random.Range(0, targetTypeCount);
  }
}