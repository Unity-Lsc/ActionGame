using SpriteFramework;
using UnityEngine;
/// <summary>
/// 战斗类
/// </summary>
public class Battle : IBattle
{

    public RoleLogicCtrl RoleLogicCtrl { get; private set; }

    public MonsterLogicCtrl MonsterLogicCtrl { get; private set; }

    /// <summary>
    /// 逻辑帧累计运行时间
    /// </summary>
    private float m_AccLogicRunTime;
    /// <summary>
    /// 下一个逻辑帧开始的时间
    /// </summary>
    private float m_NextLogicFrameTime;
    /// <summary>
    /// 逻辑帧动画缓动时间
    /// </summary>
    private float m_LogicDeltaTime;

    public void OnCreate() {
        GameEntry.UI.OpenUIForm<Joystick>();
        GameEntry.UI.OpenUIFormLua("UIBattleForm");

        RoleLogicCtrl = new RoleLogicCtrl();
        MonsterLogicCtrl = new MonsterLogicCtrl();
        RoleLogicCtrl.OnInit();
        MonsterLogicCtrl.OnInit();
    }

    public void OnUpdate() {
        m_AccLogicRunTime += Time.deltaTime;
        //当前逻辑帧时间如果大于下一个逻辑帧时间,就需要更新逻辑帧
        //追帧操作
        //控制帧数,保证所有设备逻辑帧数的一致性,并进行追帧操作
        while (m_AccLogicRunTime > m_NextLogicFrameTime) {
            OnLogicFrameUpdate();
            m_NextLogicFrameTime += LogicFrameConfig.LogicFrameInterval;
            //逻辑帧ID进行自增
            LogicFrameConfig.LogicFrameId++;
        }
        m_LogicDeltaTime = (m_AccLogicRunTime + LogicFrameConfig.LogicFrameInterval - m_NextLogicFrameTime) / LogicFrameConfig.LogicFrameInterval;
    }

    /// <summary>
    /// 更新逻辑帧
    /// </summary>
    private void OnLogicFrameUpdate() {
        RoleLogicCtrl.OnLogicFrameUpdate();
        MonsterLogicCtrl.OnLogicFrameUpdate();
    }

    public void OnDestroy() {
        
    }
}
