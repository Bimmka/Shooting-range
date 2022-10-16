using UnityEngine;

namespace Features.Level.Settings
{
  [CreateAssetMenu(fileName = "LevelSettings", menuName = "StaticData/Level/Create Level Settings", order = 52)]
  public class LevelSettings : ScriptableObject
  {
    public int TargetsOnStart = 10;
    public int TargetsToWin = 50;
    public int GameSecondsTime = 120;
  }
}