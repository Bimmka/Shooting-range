using Features.Services.UI.Factory;
using Features.Targets.Scripts.Base;
using Features.Targets.Scripts.Elements;
using Features.Targets.Scripts.Settings;
using Features.Targets.Scripts.Spawn;
using Features.UI.Data;

namespace Features.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    TargetSettings Settings(TargetType type);
    WindowInstantiateData ForWindow(WindowId id);
  }
}