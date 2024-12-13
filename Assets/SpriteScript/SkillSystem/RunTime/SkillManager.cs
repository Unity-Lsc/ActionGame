using SpriteFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    /// <summary>
    /// 技能创建者
    /// </summary>
    private LogicActor m_LogicActor;

    /// <summary>
    /// 存储技能的列表
    /// </summary>
    private List<Skill> m_SkillList = new List<Skill>();

    public SkillManager(LogicActor logicActor) {
        m_LogicActor = logicActor;
    }

    public void InitSkills(int[] skillIdArr) {
        foreach (var skillId in skillIdArr) {
            Skill skill = new Skill(skillId, m_LogicActor);
            m_SkillList.Add(skill);
        }
        GameEntry.Log("技能初始化完成  技能数量:{0}", skillIdArr.Length);
    }

    /// <summary>
    /// 释放技能
    /// </summary>
    /// <param name="skillId">释放技能的ID</param>
    /// <param name="onReleaseSkillAfter"></param>
    /// <param name="onReleaseSkillEnd"></param>
    public Skill ReleaseSkill(int skillId, BaseAction<Skill> onReleaseSkillAfter, BaseAction<Skill> onReleaseSkillEnd) {
        foreach (var item in m_SkillList) {
            if(item.SkillId == skillId) {
                item.ReleaseSkill(onReleaseSkillAfter, (skill, isComboSkill) => {
                    onReleaseSkillEnd?.Invoke(skill);
                    //如果是组合技能
                    if (isComboSkill) {

                    }
                });
                return item;
            }
        }
        GameEntry.LogError("技能ID:{0}不存在", skillId);
        return null;
    }

    public void OnLogicFrameUpdate() {
        foreach (var item in m_SkillList) {
            item.OnLogicFrameUpdate();
        }
    }

}
