using System.Collections.Generic;
using System.Linq;
using Features.Constants;
using Features.Services.UI.Factory;
using Features.Targets.Scripts.Base;
using Features.Targets.Scripts.Settings;
using Features.UI.Data;
using UnityEngine;

namespace Features.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<TargetType, TargetSettings> targetSettings;

    private Dictionary<WindowId, WindowInstantiateData> windows;
    
    public void Load()
    {
      targetSettings = Resources.Load<TargetsSettings>(ConstantValues.TargetsSettingsPath).Settings
        .ToDictionary(x => x.Type, x => x);

      windows = Resources.Load<WindowsStaticData>(ConstantValues.WindowsPath).InstantiateData
        .ToDictionary(x => x.ID, x => x);

      Resources.UnloadUnusedAssets();
    }

    public TargetSettings Settings(TargetType type) => 
      targetSettings.TryGetValue(type, out TargetSettings settings) ? settings : new TargetSettings();

    public WindowInstantiateData ForWindow(WindowId id) => 
      windows.TryGetValue(id, out WindowInstantiateData instantiateData) ? instantiateData : new WindowInstantiateData();
  }
}