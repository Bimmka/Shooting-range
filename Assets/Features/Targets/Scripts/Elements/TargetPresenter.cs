using System;
using Features.Level.Zone.Scripts;
using Features.Targets.Scripts.Base;
using Features.Targets.Scripts.HP;
using Features.Targets.Scripts.Settings;
using Features.Targets.Scripts.Spawn;
using UnityEngine;

namespace Features.Targets.Scripts.Elements
{
  [RequireComponent(typeof(TargetView))]
  public class TargetPresenter : MonoBehaviour
  {
    [SerializeField] private Rigidbody2D presenterBody;
    [SerializeField] private TargetView view;
    
    private TargetSettings settings;
    private TargetMover mover;
    private TargetHP hp;

    public TargetStatus Status { get; private set; }
    public TargetType Type => settings.Type;
    public event Action Died;
    public event Action Hiden;
    public event Action Appeared;

    public void Initialize(TargetSettings settings, TargetMover mover, TargetHP hp)
    {
      this.hp = hp;
      this.hp.Overed += OnHPOver;
      this.mover = mover;
      this.settings = settings;
      UpdateStatus(TargetStatus.Disabled);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      if (other.collider.TryGetComponent(out GameZoneEdge zoneEdge) || other.collider.TryGetComponent(out TargetPresenter trget))
      {
        mover.ChangeDirectionByCollision(other.contacts[0].normal);
      }
    }

    public void Show()
    {
      UpdateStatus(TargetStatus.Appearing);
      view.Show(OnAppear);
    }

    public void TakeDamage(int damage)
    {
      view.DisplayHit();
      hp.Decrease(damage);
    }

    public void Restore() => 
      hp.Restore();

    public void SetPosition(Vector3 position) => 
      mover.SetPosition(position);

    public void SetMoveDirection(Vector2 moveDirection) => 
      mover.SetMoveDirection(moveDirection);

    public void Disable()
    {
      Hide();
      StopMove();
      UpdateStatus(TargetStatus.Disabled);
    }

    private void StartMove() => 
      mover.StartMove();

    private void StopMove() => 
      mover.StopMove();

    private void OnHPOver()
    {
      UpdateStatus(TargetStatus.Disappearing);
      StopMove();
      NotifyAboutHide();
      Hide(OnDied);
    }

    private void Hide(Action callback = null)
    {
      presenterBody.velocity = Vector2.zero;
      presenterBody.simulated = false;
      view.Hide(callback);
    }

    private void OnAppear()
    {
      EnableRigidbody();
      UpdateStatus(TargetStatus.Moving);
      NotifyAboutAppear();
      StartMove();
    }

    private void OnDied()
    {
      UpdateStatus(TargetStatus.Disabled);
      NotifyAboutDie();
    }

    private void UpdateStatus(TargetStatus status) => 
      Status = status;

    private void EnableRigidbody() => 
      presenterBody.simulated = true;

    private void NotifyAboutAppear() => 
      Appeared?.Invoke();

    private void NotifyAboutHide() => 
      Hiden?.Invoke();

    private void NotifyAboutDie() => 
      Died?.Invoke();
  }
}