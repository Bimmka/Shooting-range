using System;
using Features.Targets.Scripts.Base;
using Features.Targets.Scripts.HP;
using Features.Targets.Scripts.Settings;
using UnityEngine;

namespace Features.Targets.Scripts.Elements
{
  public class TargetPresenter : MonoBehaviour
  {
    private TargetSettings settings;
    private TargetMover mover;
    private TargetHP hp;

    public TargetStatus Status { get; private set; }
    public TargetType Type => settings.Type;
    public string ID { get; private set; }

    public event Action Died;
    public event Action Hiden;
    public event Action Appeared;

    public void Initialize(TargetSettings settings, string id, TargetMover mover, TargetHP hp)
    {
      this.hp = hp;
      this.hp.Overed += OnHPOver;
      this.mover = mover;
      this.settings = settings;
      ID = id;
      Status = TargetStatus.Disabled;
    }

    public void Move() => 
      mover.Move();

    public void TakeDamage(int damage) => 
      hp.Decrease(damage);

    public void Restore() => 
      hp.Restore();

    private void OnHPOver()
    {
      
    }

    private void NotifyAboutAppear() => 
      Appeared?.Invoke();

    private void NotifyAboutHide() => 
      Hiden?.Invoke();

    private void NotifyAboutDie() => 
      Died?.Invoke();
  }
}