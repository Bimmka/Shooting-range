using System.Collections.Generic;

namespace Features.Bullet.Scripts
{
  public class BulletsContainer
  {
    private const int StartBulletsCount = 10;
    
    private readonly BulletFactory factory;
    private readonly Queue<BulletPresenter> bullets;
    private readonly List<BulletPresenter> activeBullets;

    public BulletsContainer(BulletFactory factory)
    {
      this.factory = factory;
      bullets = new Queue<BulletPresenter>(StartBulletsCount);
      activeBullets = new List<BulletPresenter>(StartBulletsCount);
      SpawnStartBullets();
    }

    public void Cleanup()
    {
      BulletPresenter presenter;
      while (IsHaveBulletInPool())
      {
        presenter = bullets.Dequeue();
        presenter.Hitted -= OnBulletHit;
      }

      for (int i = 0; i < activeBullets.Count; i++)
      {
        activeBullets[i].Hitted -= OnBulletHit;
      }
    }

    public BulletPresenter Bullet()
    {
      BulletPresenter bullet;
      bullet = IsHaveBulletInPool() ? bullets.Dequeue() : CreateBullet();
      activeBullets.Add(bullet);
      return bullet;
    }

    private void SpawnStartBullets()
    {
      for (int i = 0; i < StartBulletsCount; i++)
      {
        bullets.Enqueue(CreateBullet());
      }
    }

    private BulletPresenter CreateBullet()
    {
      BulletPresenter bulletPresenter = factory.Spawn();
      bulletPresenter.Hitted += OnBulletHit;
      return bulletPresenter;
    }

    private void OnBulletHit(BulletPresenter bulletPresenter)
    {
      bulletPresenter.Hide();
      bullets.Enqueue(bulletPresenter);
      activeBullets.Remove(bulletPresenter);
    }

    private bool IsHaveBulletInPool() => 
      bullets.Count > 0;
  }
}