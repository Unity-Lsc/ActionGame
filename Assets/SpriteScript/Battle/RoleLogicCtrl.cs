using SpriteFramework;
using UnityEngine;

/// <summary>
/// 角色逻辑控制
/// </summary>
public class RoleLogicCtrl
{

    public RoleLogic RoleLogic { get; private set; }

    /// <summary>
    /// 初始化场景中的角色
    /// </summary>
    public void OnInit() {

        GameObject roleObj = UnityUtils.LoadPrefabClone(SFConstDefine.RoleRoot + "1000");
        RoleRender roleRender = roleObj.GetComponent<RoleRender>();
        RoleLogic roleLogic = new RoleLogic(1000, roleRender);
        RoleLogic = roleLogic;
        roleRender.SetLogicObject(roleLogic);

        //初始化渲染层和逻辑层
        roleLogic.OnInit();
        roleRender.OnInit();

    }

    public void OnLogicFrameUpdate() {
        RoleLogic.OnLogicFrameUpdate();
    }

}
