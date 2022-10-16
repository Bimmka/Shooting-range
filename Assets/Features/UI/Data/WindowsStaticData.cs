using UnityEngine;

namespace Features.UI.Data
{
  [CreateAssetMenu(fileName = "WindowsStaticData", menuName = "StaticData/UI/Create Windows Instantiate Data", order = 52)]
  public class WindowsStaticData : ScriptableObject
  {
    public WindowInstantiateData[] InstantiateData;
  }
}