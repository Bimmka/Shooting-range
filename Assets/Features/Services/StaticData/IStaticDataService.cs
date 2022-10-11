using Features.Targets.Scripts.Base;
using Features.Targets.Scripts.Elements;
using Features.Targets.Scripts.Settings;

namespace Features.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    TargetSettings Settings(TargetType type);
  }
}