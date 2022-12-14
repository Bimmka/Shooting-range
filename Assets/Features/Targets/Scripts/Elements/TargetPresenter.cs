using System;
using Features.Level.Zone.Scripts;
using Features.Services.Pause;
using Features.Targets.Scripts.HP;
using Features.Targets.Scripts.Settings;
using Features.Targets.Scripts.Spawn;
using UnityEngine;

namespace Features.Targets.Scripts.Elements
{
  [RequireComponent(typeof(TargetView))]
  public class TargetPresenter : MonoBehaviour, IPaused
  {
    [SerializeField] private Rigidbody2D presenterBody;
    [SerializeField] private TargetView view;
    
    private TargetSettings settings;
    private TargetMover mover;
    private TargetHP hp;
    private IPauseService pauseService;

    public TargetStatus Status { get; private set; }
    public TargetType Type => settings.Type;
    public event Action Died;

    public void Construct(IPauseService pauseService)
    {
      this.pauseService = pauseService;
      pauseService.Register(this);
    }

    public void Initialize(TargetSettings settings, TargetMover mover, TargetHP hp)
    {
      this.hp = hp;
      this.hp.Overed += OnHPOver;
      this.mover = mover;
      this.settings = settings;
      UpdateStatus(TargetStatus.Disabled);
    }

    private void OnDestroy() => 
      pauseService.Unregister(this);

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

    public void Pause()
    {
      StopMove();
      DisableRigidbody();
    }

    public void Unpause()
    {
      EnableRigidbody();
      StartMove();
    }

    private void StartMove() => 
      mover.StartMove();

    private void StopMove() => 
      mover.StopMove();

    private void OnHPOver()
    {
      UpdateStatus(TargetStatus.Disappearing);
      StopMove();
      Hide(OnDied);
    }

    private void Hide(Action callback = null)
    {
      DisableRigidbody();
      view.Hide(callback);
    }

    private void OnAppear()
    {
      EnableRigidbody();
      UpdateStatus(TargetStatus.Moving);
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

    private void DisableRigidbody()
    {
      presenterBody.velocity = Vector2.zero;
      presenterBody.simulated = false;
    }

    private void NotifyAboutDie() => 
      Died?.Invoke();
  }
}