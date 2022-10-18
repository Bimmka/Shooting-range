using System;
using Features.Bullet.Data;
using Features.Services.Pause;
using UnityEngine;

namespace Features.Bullet.Scripts
{
  [RequireComponent(typeof(BulletView))]
  public class BulletPresenter : MonoBehaviour, IPaused
  {
    [SerializeField] private BulletView view;
    [SerializeField] private BulletMoveSettings moveSettings;
    [SerializeField] private BulletHitterSettings hitterSettings;
    
    private BulletModel model;
    private IPauseService pauseService;

    public event Action<BulletPresenter> Hitted;

    public void Construct(IPauseService pauseService)
    {
      this.pauseService = pauseService;
      BulletMover mover = new BulletMover(view.transform, moveSettings);
      BulletHitter hitter = new BulletHitter(view.transform, hitterSettings);
      model = new BulletModel(mover, hitter);
      model.Hitted += OnHit;
      pauseService.Register(this);
    }

    private void OnDestroy()
    {
      model.Hitted -= OnHit;
      model.Cleanup();
      pauseService.Unregister(this);
    }

    public void Pause() => 
      model.Pause();

    public void Unpause() => 
      model.Unpause();

    public void SetPosition(Vector3 position) => 
      model.SetPosition(position);

    public void Show() => 
      view.Show();

    public void Hide() => 
      view.Hide();

    public void SetDamage(int damage) => 
      model.SetDamage(damage);

    public void Move(Vector3 startPosition, Vector3 endPosition) => 
      model.Move(startPosition, endPosition);

    private void OnHit() => 
      Hitted?.Invoke(this);
  }
}