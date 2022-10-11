using System.Collections.Generic;
using System.Linq;
using Features.Constants;
using Features.Targets.Scripts.Base;
using Features.Targets.Scripts.Settings;
using UnityEngine;

namespace Features.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<TargetType, TargetSettings> targetSettings;
    
    public void Load()
    {
      targetSettings = Resources.Load<TargetsSettings>(ConstantValues.TargetsSettingsPath).Settings
        .ToDictionary(x => x.Type, x => x);

      Resources.UnloadUnusedAssets();
    }

    public TargetSettings Settings(TargetType type) => 
      targetSettings.TryGetValue(type, out TargetSettings settings) ? settings : new TargetSettings();
  }
}