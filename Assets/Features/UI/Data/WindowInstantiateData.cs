﻿using System;
using Features.Services.UI.Factory;
using Features.UI.Windows.Base;

namespace Features.UI.Data
{
  [Serializable]
  public struct WindowInstantiateData
  {
    public WindowId ID;
    public BaseWindow Window;
  }
}