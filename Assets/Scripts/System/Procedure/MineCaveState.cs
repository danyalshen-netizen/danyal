using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MineCaveState : GameProcedureStateBase
{
    public override void OnEnter()
    {
        Debug.Log("[GameProcedure] 进入 MineCaveState");
        SceneManager.sceneLoaded += OnMineCaveLoaded;
        SceneManager.LoadScene("MineCave");
    }

    public override void OnExit()
    {
        SceneManager.sceneLoaded -= OnMineCaveLoaded;
        Debug.Log("[GameProcedure] 退出 MineCaveState");
    }

    private void OnMineCaveLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MineCave") return;

        SceneManager.sceneLoaded -= OnMineCaveLoaded;
        Debug.Log("[GameProcedure] MineCave场景加载完成");

        foreach (var root in scene.GetRootGameObjects())
        {
            var context = root.GetComponent<MineCaveSceneContext>();
            if (context == null) continue;

            CreatePlayer(context);
            return;
        }

        Debug.LogError("[MineCaveState] 场景根物体中未找到 MineCaveSceneContext。");
    }

    private void CreatePlayer(MineCaveSceneContext context)
    {
        if (context.bornPoint == null)
        {
            Debug.LogError("[MineCaveState] 请配置 MineCaveSceneContext 的 bornPoint。");
            return;
        }

        var roleIds = this.GetModel<IRoleRuntimeModel>().GetAllRoleRuntimeIds();
        if (roleIds.Count == 0)
        {
            Debug.LogError("[MineCaveState] 没有可创建的运行时角色。");
            return;
        }

        var roleSystem = this.GetSystem<IRoleInstanceSystem>();
        var player = roleSystem.CreateRoleInstance(
            roleIds[0], context.bornPoint.position, context.bornPoint.rotation);
        if (player == null) return;

        // 先接管角色，再切换到其第一人称视角。
        roleSystem.AddPlayerMoveController(player);
        if (player.FirstViewCinema == null)
        {
            Debug.LogError("[MineCaveState] 默认角色未配置 FirstViewCinema。");
            return;
        }

        this.GetSystem<ICinemaChineCameraSystem>().SetCinemaChineCamera(player.FirstViewCinema);
    }
}
