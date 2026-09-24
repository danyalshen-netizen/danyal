using QFramework;
using UnityEngine;

public class GoldMineArchitecture : Architecture<GoldMineArchitecture>
{
    protected override void Init()
    {
        Debug.Log("GoldMine architecture initializing.");

        // 注册Utility
        this.RegisterUtility<IJsonStorage>(new JsonStorage());
        this.RegisterUtility<IResourceStorage>(new ResourceStorage());
        this.RegisterUtility<IRoleConfigProvider>(new RoleConfigProvider(this.GetUtility<IJsonStorage>()));
        this.RegisterUtility<IWeaponConfigProvider>(new WeaponConfigProvider(this.GetUtility<IJsonStorage>()));
        this.RegisterUtility<IMonsterConfigProvider>(new MonsterConfigProvider(this.GetUtility<IJsonStorage>()));

        // 注册Model
        this.RegisterModel<IRoleRuntimeModel>(new RoleRuntimeModel());
        this.RegisterModel<IPackageModel>(new PackageModel());
        this.RegisterModel<IWareHouseModel>(new WareHouseModel());
        this.RegisterModel<IMonsterRuntimeModel>(new MonsterRuntimeModel());

        // 注册System
        this.RegisterSystem<ICameraSystem>(new CameraSystem());
        this.RegisterSystem<IGameProcedureSystem>(new GameProcedureSystem());
        this.RegisterSystem<ICinemaChineCameraSystem>(new CinemaChineCameraSystem());
        this.RegisterSystem<IRoleRuntimeSystem>(new RoleRuntimeSystem());
        this.RegisterSystem<IRoleInstanceSystem>(new RoleInstanceSystem());
        this.RegisterSystem<IPackageSystem>(new PackageSystem());
        this.RegisterSystem<IWeaponInstanceSystem>(new WeaponInstanceSystem());
        this.RegisterSystem<IWareHouseSystem>(new WareHouseSystem());
        this.RegisterSystem<IGamePauseSystem>(new GamePauseSystem());
        this.RegisterSystem<IMonsterRuntimeSystem>(new MonsterRuntimeSystem());

        Debug.Log("GoldMine architecture initialized.");
    }
}
