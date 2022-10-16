using System;
using Features.Bullet.Data;
using UnityEngine;

namespace Features.Bullet.Scripts
{
  [RequireComponent(typeof(BulletView))]
  public class BulletPresenter : MonoBehaviour
  {
    [SerializeField] private BulletView view;
    [SerializeField] private BulletMoveSettings moveSettings;
    [SerializeField] private BulletHitterSettings hitterSettings;
    
    private BulletModel model;

    public event Action<BulletPresenter> Hitted;

    private void OnDestroy()
    {
      model.Hitted -= OnHit;
      model.Cleanup();
    }

    public void Initialize()
    {
      BulletMover mover = new BulletMover(view.transform, moveSettings);
      BulletHitter hitter = new BulletHitter(view.transform, hitterSettings);
      model = new BulletModel(mover, hitter);
      model.Hitted += OnHit;
    }

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