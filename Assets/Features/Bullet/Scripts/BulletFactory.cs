using Features.Services.Assets;
using Features.Services.Pause;
using UnityEngine;

namespace Features.Bullet.Scripts
{
  public class BulletFactory
  {
    private readonly IAssetProvider assetProvider;
    private readonly Transform spawnParent;
    private readonly BulletPresenter bulletPrefab;
    private readonly IPauseService pauseService;

    public BulletFactory(IAssetProvider assetProvider, Transform spawnParent, BulletPresenter bulletPrefab, IPauseService pauseService)
    {
      this.assetProvider = assetProvider;
      this.spawnParent = spawnParent;
      this.bulletPrefab = bulletPrefab;
      this.pauseService = pauseService;
    }

    public BulletPresenter Spawn()
    {
      BulletPresenter bullet = assetProvider.Instantiate(bulletPrefab, spawnParent);
      bullet.Construct(pauseService);
      return bullet;
    }
  }
}