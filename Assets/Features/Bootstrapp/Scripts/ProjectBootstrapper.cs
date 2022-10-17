using Features.Services.Assets;
using Features.Services.StaticData;
using Zenject;

namespace Features.Bootstrapp.Scripts
{
  public class ProjectBootstrapper : MonoInstaller
  {
    public override void InstallBindings()
    {
      BindStaticData();
      BindAssetProvider();
    }

    private void BindStaticData()
    {
      IStaticDataService staticDataService = new StaticDataService();
      staticDataService.Load();
      Container.Bind<IStaticDataService>().FromInstance(staticDataService).AsSingle();
    }
    private void BindAssetProvider() => 
      Container.Bind<IAssetProvider>().To<AssetProvider>().FromNew().AsSingle();
  }
}