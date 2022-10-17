using System;
using Features.Targets.Scripts.Base;
using UnityEngine;

namespace Features.Targets.Scripts.Settings
{
  [Serializable]
  public struct TargetSettings
  {
    public TargetType Type;
    public float MoveSpeed;
    public GameObject View;
    public int MaxHp;
  }
}