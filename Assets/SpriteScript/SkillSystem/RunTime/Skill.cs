using SpriteFramework;

public enum SkillState
{
    None,
    Before,//技能前摇
    After,//技能后摇
    End,//技能结束
}

/// <summary>
/// 技能类
/// </summary>
public partial class Skill
{
    /// <summary>
    /// 技能ID
    /// </summary>
    public int SkillId;

    //技能的释放者
    private LogicActor m_SkillCreator;

    //技能配置数据
    private SkillDataConfig m_SkillDataConfig;
    //当前逻辑帧
    private int m_CurLogicFrame = 0;
    //当前累计运行时间
    private int m_CurLogicFrameAccTime;

    /// <summary>
    /// 释放技能后摇回调
    /// </summary>
    public BaseAction<Skill> OnReleaseSkillAfter;
    /// <summary>
    /// 释放技能结束回调(bool->是否是组合技能)
    /// </summary>
    public BaseAction<Skill, bool> OnReleaseSkillEnd;
    /// <summary>
    /// 当前的技能状态
    /// </summary>
    public SkillState CurSkillState = SkillState.None;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="skillId">技能ID</param>
    /// <param name="logicActor">技能的施放者</param>
    public Skill(int skillId, LogicActor logicActor) {
        SkillId = skillId;
        m_SkillCreator = logicActor;
        m_SkillDataConfig = GameEntry.Resource.LoadAsset<SkillDataConfig>(SFConstDefine.SkillConfigDataRoot + skillId);
    }

    /// <summary>
    /// 释放技能
    /// </summary>
    /// <param name="onReleaseSkillAfter">释放技能后摇回调</param>
    /// <param name="onReleaseSkillEnd">释放技能结束回调(bool->是否是蓄力技能)</param>
    public void ReleaseSkill(BaseAction<Skill> onReleaseSkillAfter, BaseAction<Skill, bool> onReleaseSkillEnd) {
        OnReleaseSkillAfter = onReleaseSkillAfter;
        OnReleaseSkillEnd = onReleaseSkillEnd;

        PlayAnim();
        CurSkillState = SkillState.Before;
        SkillBefore();
    }

    /// <summary>
    /// 播放技能动画
    /// </summary>
    public void PlayAnim() {
        m_SkillCreator.PlayAnim(m_SkillDataConfig.CharacterCfg.SkillAnimClip);
    }

    /// <summary>
    /// 技能前摇
    /// </summary>
    public void SkillBefore() {
        m_CurLogicFrame = 0;
        m_CurLogicFrameAccTime = 0;
    }

    /// <summary>
    /// 技能后摇
    /// </summary>
    public void SkillAfter() {
        CurSkillState = SkillState.After;
    }

    /// <summary>
    /// 技能结束
    /// </summary>
    public void SkillEnd() {
        CurSkillState = SkillState.End;
        OnReleaseSkillEnd?.Invoke(this, false);
    }

    /// <summary>
    /// 逻辑帧更新
    /// </summary>
    public void OnLogicFrameUpdate() {

        if (CurSkillState == SkillState.None) return;

        //计算累计运行时间
        m_CurLogicFrameAccTime = m_CurLogicFrame * LogicFrameConfig.LogicFrameIntervalMS;

        //判断是否进入技能后摇的时间
        if(CurSkillState == SkillState.Before && m_CurLogicFrameAccTime >= m_SkillDataConfig.SkillCfg.ShakeBeforeTimeMS) {
            SkillAfter();
        }

        //更新不同配置的逻辑帧 处理不同配置的逻辑
        //1.更新特效逻辑帧
        OnLogicFrameUpdateEffect();
        //2.更新伤害逻辑帧
        //3.更新行动逻辑帧
        //4.更新音效逻辑帧
        //5.更新子弹逻辑帧

        if(CurSkillState != SkillState.End && m_CurLogicFrame >= m_SkillDataConfig.CharacterCfg.LogicFrame) {
            SkillEnd();
        }

        //逻辑帧自增
        m_CurLogicFrame++;
    }

}
