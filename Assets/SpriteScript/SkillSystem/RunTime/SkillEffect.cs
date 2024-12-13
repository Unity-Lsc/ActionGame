using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//技能特效的分布类
public partial class Skill
{
    /// <summary>
    /// 存储释放的技能特效  int->技能特效配置(SkillEffectConfig)的哈希值
    /// </summary>
    private Dictionary<int, SkillEffectLogic> m_ReleaseSkillDict = new Dictionary<int, SkillEffectLogic>();

    /// <summary>
    /// 逻辑帧技能特效的更新
    /// </summary>
    public void OnLogicFrameUpdateEffect() {
        if(m_SkillDataConfig.EffectCfgList != null && m_SkillDataConfig.EffectCfgList.Count > 0) {
            foreach (var item in m_SkillDataConfig.EffectCfgList) {
                //触发帧
                if(item.Effect != null && m_CurLogicFrame == item.TriggerFrame) {
                    //生成技能特效
                    DestroySkillEffect(item);
                    GameObject effectObj = UnityUtils.InstantiateObj(item.Effect);
                    //创建技能特效的渲染层和逻辑层
                    SkillEffectRender effectRender = effectObj.GetOrAddCompponent<SkillEffectRender>();
                    SkillEffectLogic effectLogic = new SkillEffectLogic(LogicObjectType.Effect, item, effectRender, m_SkillCreator);
                    effectRender.SetLogicObject(effectLogic);

                    m_ReleaseSkillDict[item.GetHashCode()] = effectLogic;
                }
                //结束帧
                if(m_CurLogicFrame == item.EndFrame) {
                    DestroySkillEffect(item);
                }
            }
        }
    }

    /// <summary>
    /// 销毁技能特效
    /// </summary>
    private void DestroySkillEffect(SkillEffectConfig effectConfig) {
        int key = effectConfig.GetHashCode();
        m_ReleaseSkillDict.TryGetValue(key, out SkillEffectLogic effectLogic);
        if (effectLogic != null) {
            m_ReleaseSkillDict.Remove(key);
            effectLogic.OnRelease();
        }
    }

}
