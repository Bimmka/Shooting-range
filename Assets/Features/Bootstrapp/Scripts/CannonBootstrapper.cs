using Features.Bullet.Scripts;
using Features.Cannon.Scripts;
using Features.Cannon.Scripts.Aim;
using Features.Cannon.Scripts.Base;
using Features.Input;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp.Scripts
{
  public class CannonBootstrapper : MonoInstaller
  {
    [SerializeField] private CannonPresenter cannonPrefab;
    [SerializeField] private Transform cannonSpawnParent;
    [SerializeField] private Transform aimSpawnParent;
    [SerializeField] private AimView aimViewPrefab;
    [SerializeField] private BulletPresenter bulletPrefab;
    [SerializeField] private Transform bulletSpawnParent;

    public override void InstallBindings()
    {
      BindPlayerInput();
      BindMainCamera();
      BindAimView();
      BindBulletsContainer();
      BindBulletFactory();
      BindCannonPresenter();
    }

    private void BindPlayerInput() => 
      Container.Bind<IPlayerInputService>().To<PlayerInputService>().FromNew().AsSingle();

    private void BindMainCamera() => 
      Container.Bind<Camera>().WithId("Main Camera").FromInstance(Camera.main).AsSingle();

    private void BindAimView() => 
      Container.Bind<AimView>().ToSelf().FromComponentOn(SpawnAimView).AsSingle();

    private void BindBulletsContainer() => 
      Container.Bind<BulletsContainer>().ToSelf().FromNew().AsSingle();

    private void BindBulletFactory() => 
      Container.Bind<BulletFactory>().ToSelf().FromNew().AsSingle().WithArguments(bulletPrefab, bulletSpawnParent);

    private void BindCannonPresenter() =>
      Container.Bind<CannonPresenter>().ToSelf().FromComponentOn(SpawnCannon).AsSingle();

    private GameObject SpawnCannon(InjectContext context) => 
      Container.InstantiatePrefab(cannonPrefab, cannonSpawnParent);
    
    private GameObject SpawnAimView(InjectContext context) => 
      Container.InstantiatePrefab(aimViewPrefab, aimSpawnParent);
  }
}