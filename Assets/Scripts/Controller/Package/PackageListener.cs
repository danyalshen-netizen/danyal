using QFramework;
using UnityEngine;

/// <summary>
/// 背包监听器：监听全局快捷键 HotKeyUtility.OpenPackage（默认 Q），
/// 通过 UIKit 打开/关闭背包面板 PackagePanel。
/// </summary>
public class PackageListener : MonoBehaviour, IController
{
    private void Update()
    {
        if (!Input.GetKeyDown(HotKeyUtility.OpenPackage))
        {
            return;
        }

        TogglePackage();
    }

    /// <summary>切换背包面板显隐，保留预加载的面板实例。</summary>
    private void TogglePackage()
    {
        var panel = UIKit.GetPanel<PackagePanel>();
        if (panel != null && panel.gameObject.activeSelf)
        {
            UIKit.HidePanel<PackagePanel>();
        }
        else if (panel != null)
        {
            UIKit.ShowPanel<PackagePanel>();
        }
        else
        {
            UIKit.OpenPanel<PackagePanel>(prefabName: "resources://UI/Panel/packagepanel");
        }
    }

    public IArchitecture GetArchitecture()
    {
        return GoldMineArchitecture.Interface;
    }
}
