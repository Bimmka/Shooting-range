using Features.Targets.Scripts.Elements;
using UnityEngine;

namespace Features.Targets.Scripts.Base
{
  public class TargetsSpawner
  {
    private readonly TargetsContainer targetsContainer;
    private readonly TargetsFactory factory;

    public TargetsSpawner(TargetsContainer targetsContainer, TargetsFactory factory)
    {
      this.targetsContainer = targetsContainer;
      this.targetsContainer.PresenterDied += OnPresenterDied;
      this.factory = factory;
    }

    public void Cleanup()
    {
      targetsContainer.PresenterDied -= OnPresenterDied;
      targetsContainer.Cleanup();
    }

    public void SpawnStartedTargets(int count)
    {
      TargetPresenter presenter;
      for (int i = 0; i < count; i++)
      {
        presenter = factory.Spawn(TargetType.Hard, Vector3.zero);
        targetsContainer.AddTarget(presenter);
      }
    }

    private void OnPresenterDied()
    {
      
    }
  }
}