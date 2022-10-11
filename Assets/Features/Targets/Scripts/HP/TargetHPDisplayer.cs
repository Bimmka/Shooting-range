using System;
using UnityEngine;

namespace Features.Targets.Scripts.HP
{
  public class TargetHPDisplayer : MonoBehaviour
  {
    private TargetHP hp;

    public void Construct(TargetHP hp)
    {
      this.hp = hp;
      this.hp.Changed += Display;
    }

    private void OnDestroy()
    {
      hp.Changed -= Display;
    }

    private void Display(int currentHealth, int maxHealth)
    {
      
    }
  }
}