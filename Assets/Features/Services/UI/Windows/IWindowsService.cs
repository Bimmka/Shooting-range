using Features.Services.UI.Factory;
using Features.UI.Windows.Base;
using Features.UI.Windows.Base.Scripts;

namespace Features.Services.UI.Windows
{
  public interface IWindowsService
  {
    void Open(WindowId windowId);
    void Close(WindowId windowId);
    BaseWindow Window(WindowId windowId);
    void Cleanup();
  }
}