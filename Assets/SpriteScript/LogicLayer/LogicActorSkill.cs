using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//逻辑演员 技能
public partial class LogicActor
{

    private SkillManager m_SkillManager;

    private int[] m_NormalSkillIdArr = new int[] { 1001, };

    //存储已经释放的技能列表
    private List<Skill> m_ReleasedSkillList = new List<Skill>();

    /// <summary>
    /// 初始化技能
    /// </summary>
    private void InitActorSkill() {
        m_SkillManager = new SkillManager(this);
        m_SkillManager.InitSkills(m_NormalSkillIdArr);
    }

    public void ReleaseSkill(int skillId) {
        Skill skill = m_SkillManager.ReleaseSkill(skillId, OnReleaseSkillAfter, OnReleaseSkillEnd);
        if(skill != null) {
            m_ReleasedSkillList.Add(skill);
            if(m_ReleasedSkillList.Count > 0) {
                ActionState = LogicObjectActionState.Skilling;
            }
        }
    }

    private void OnReleaseSkillAfter(Skill skill) {

    }

    private void OnReleaseSkillEnd(Skill skill) {
        if(skill != null) {
            m_ReleasedSkillList.Remove(skill);
            if(m_ReleasedSkillList.Count == 0) {
                ActionState = LogicObjectActionState.Idle;
            }
        }
    }

    /// <summary>
    /// 逻辑帧技能更新
    /// </summary>
    public void OnLogicFrameUpdateSkill() {
        m_SkillManager.OnLogicFrameUpdate();
    }

}
