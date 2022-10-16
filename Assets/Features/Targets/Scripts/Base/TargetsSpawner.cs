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
      for (int i = 0; i < count; i++)
      {
        CreateNewTarget(RandomType(), Vector3.up);
      }
    }

    public void RespawnTargets(int count)
    {
      for (int i = 0; i < count; i++)
      {
        SpawnTarget();
      }
    }

    public void SpawnTarget()
    {
      TargetType type = RandomType();
      if (targetsContainer.IsContainsDisabledPresenter(type))
        factory.RespawnTarget(targetsContainer.PresenterOrNull(type), Vector3.right * 5);
      else
        CreateNewTarget(type, Vector3.zero);
    }

    private TargetType RandomType() => 
      (TargetType)Random.Range(0, targetTypeCount);

    private void CreateNewTarget(TargetType type, Vector3 position)
    {
      targetsContainer.AddTarget(factory.Spawn(type, position));
    }
  }
}