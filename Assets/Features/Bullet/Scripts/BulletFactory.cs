using Features.Services.Assets;
using UnityEngine;

namespace Features.Bullet.Scripts
{
  public class BulletFactory
  {
    private readonly IAssetProvider assetProvider;
    private readonly Transform spawnParent;
    private readonly BulletPresenter bulletPrefab;

    public BulletFactory(IAssetProvider assetProvider, Transform spawnParent, BulletPresenter bulletPrefab)
    {
      this.assetProvider = assetProvider;
      this.spawnParent = spawnParent;
      this.bulletPrefab = bulletPrefab;
    }

    public BulletPresenter Spawn()
    {
      BulletPresenter bullet = assetProvider.Instantiate(bulletPrefab, spawnParent);
      bullet.Initialize();
      return bullet;
    }
  }
}