using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// 绘制技能编辑器中的Skill Damage区域
/// </summary>
[System.Serializable]
public class SkillDamegeConfig
{
    [LabelText("触发帧")]
    public int TriggerFrame;
    [LabelText("结束帧")]
    public int EndFrame;
    [LabelText("触发间隔(单位:ms) value=0表示只触发一次伤害, >0则表示触发间隔")]
    public int TriggerInterval;
    [LabelText("是否跟随特效移动")]
    public bool IsFollowEffect;
    [LabelText("伤害类型")]
    public DamageType DamageType;
    [LabelText("伤害倍率")]
    public int DamageRate;
    [LabelText("伤害检测方式")][OnValueChanged("OnDetectionTypeChanged")]
    public DamageDetectionType DamageDecectionType;
    [LabelText("Box碰撞体的宽高")][ShowIf("m_IsBox3D")]
    public Vector3 BoxSize = Vector3.one;
    [LabelText("Box碰撞体的偏移值")][ShowIf("m_IsBox3D")]
    public Vector3 BoxOffset = Vector3.zero;
    [LabelText("圆球碰撞体偏移值")][ShowIf("m_IsSphere3D")]
    public Vector3 SphereOffset = new Vector3(0, 0.9f, 0);
    [LabelText("圆球碰撞体检测半径")][ShowIf("m_IsSphere3D")]
    public float Radius = 1.0f;
    [LabelText("圆球检测的半径高度")][ShowIf("m_IsSphere3D")]
    public float RadiusHeight = 0;
    [LabelText("碰撞体未知类型")]
    public ColliderPosType ColliderPosType = ColliderPosType.FollowDir;
    [LabelText("伤害触发目标")]
    public TargetType TargetType;
    [TitleGroup("附加Buff", "伤害生效的一瞬间,附加指定的多个Buff")]
    public int[] AddBuffs;
    [TitleGroup("触发后续技能", "造成伤害后且技能释放完毕后触发的技能")]
    public int TriggerSkillId;

    //是否显示3D碰撞体
    private bool m_IsBox3D;
    //是否显示3D圆球碰撞体
    private bool m_IsSphere3D;

    private void OnDetectionTypeChanged(DamageDetectionType type) {
        m_IsBox3D = type == DamageDetectionType.Box3D;
        m_IsSphere3D = type == DamageDetectionType.Sphere3D;
    }

}

/// <summary>
/// 伤害类型
/// </summary>
public enum DamageType
{
    [LabelText("无伤害")] None = 0,
    [LabelText("物理伤害")] PhysicalDamage,
    [LabelText("魔法伤害")] MagicalDamage,
}

/// <summary>
/// 伤害监测类型
/// </summary>
public enum DamageDetectionType
{
    [LabelText("不检测")] None = 0,
    [LabelText("3D盒子 碰撞检测")] Box3D,
    [LabelText("3D圆球 碰撞检测")] Sphere3D,
    [LabelText("3D圆柱 碰撞检测")] Cylinder3D,
    [LabelText("半径的距离")] RadiusDistance,
    [LabelText("所有目标")] AllTarget,
}

/// <summary>
/// 碰撞体的位置类型
/// </summary>
public enum ColliderPosType
{
    [LabelText("跟随角色朝向")] FollowDir = 0,
    [LabelText("跟随角色位置")] FollowPos,
    [LabelText("中心坐标")] CenterPos,
    [LabelText("目标位置")] TargetPos,
}

public enum TargetType
{
    [LabelText("无配置")] None = 0,
    [LabelText("队友")] Teammate,
    [LabelText("敌人")] Enemy,
    [LabelText("自身")] Self,
    [LabelText("所有对象")] AllObject,
}
