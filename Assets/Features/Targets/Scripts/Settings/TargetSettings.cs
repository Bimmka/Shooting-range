using System;
using Features.Targets.Scripts.Animations;
using Features.Targets.Scripts.Base;
using Features.Targets.Scripts.Spawn;
using UnityEngine;

namespace Features.Targets.Scripts.Settings
{
  [Serializable]
  public struct TargetSettings
  {
    public TargetType Type;
    public float MoveSpeed;
    public TargetDissolver View;
    public int MaxHp;
  }
}