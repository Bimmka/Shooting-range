using System;
using Features.Services.UI.Windows;
using Features.UI.Windows.Base;
using Features.UI.Windows.Base.Scripts;

namespace Features.Services.UI.Factory.BaseUI
{
  public interface IUIFactory 
  {
    event Action<WindowId,BaseWindow> Spawned;
    void CreateWindow(WindowId id, IWindowsService windowsService);
  }
}