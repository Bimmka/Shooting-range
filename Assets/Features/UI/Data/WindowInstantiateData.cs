using System;
using Features.Services.UI.Factory;
using Features.UI.Windows.Base;
using Features.UI.Windows.Base.Scripts;

namespace Features.UI.Data
{
  [Serializable]
  public struct WindowInstantiateData
  {
    public WindowId ID;
    public BaseWindow Window;
  }
}