using Features.Level.Zone.Data;
using Features.Targets.Scripts.Base;
using Features.Targets.Scripts.Elements;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp.Scripts
{
  public class TargetsBootstrapper : MonoInstaller
  {
    [SerializeField] private Transform targetSpawnParent;
    [SerializeField] private TargetPresenter targetPrefab;
    [SerializeField] private TargetsZoneBoundsSettings zoneBoundsSettings;
    
    public override void InstallBindings()
    {
      BindTargetsContainer();
      BindTargetsFactory();
      BindTargetsSpawner();
      BindTargetsPositionCalculator();
    }

    private void BindTargetsContainer() => 
      Container.Bind<TargetsContainer>().ToSelf().FromNew().AsSingle();

    private void BindTargetsFactory() => 
      Container.Bind<TargetsFactory>().ToSelf().FromNew().AsSingle().WithArguments(targetSpawnParent, targetPrefab);

    private void BindTargetsSpawner() => 
      Container.Bind<TargetsSpawner>().ToSelf().FromNew().AsSingle();
    
    private void BindTargetsPositionCalculator() => 
      Container.Bind<TargetSpawnPositionCalculator>().ToSelf().FromNew().AsSingle().WithArguments(zoneBoundsSettings, targetSpawnParent);
  }
}