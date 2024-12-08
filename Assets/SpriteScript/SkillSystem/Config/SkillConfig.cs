using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 绘制技能编辑器中的Skill区域
/// </summary>
[HideMonoScript]
[System.Serializable]
public class SkillConfig
{
    //是否显示蓄力技能数据
    private bool m_IsShowStockPileData = false;

    //是否显示技能引导
    private bool m_IsShowSkillGuide = false;

    [AssetList][LabelText("技能图标"), PreviewField(70, ObjectFieldAlignment.Left), SuffixLabel("技能图标", true)]
    public Sprite SkillIcon;

    //************************************************技能参数************************************************
    [LabelText("技能ID")]
    public int Id;
    [LabelText("技能名称")]
    public string Name;
    [LabelText("技能所需蓝量")]
    public int NeedMagicValue;
    [LabelText("技能前摇时间(ms)")]
    public int ShakeBeforeTimeMS;
    [LabelText("技能攻击持续时间(ms)")]
    public int AttackDurationMS;
    [LabelText("技能攻击后摇时间(ms)")]
    public int ShakeAfterTimeMS;
    [LabelText("技能冷却时间(ms)")]
    public int CDTimeMS;

    [LabelText("技能类型")][OnValueChanged("OnSkillTypeChange")]
    public SkillType SkillType;
    [LabelText("蓄力阶段配置(若第一阶段触发时间不为0,则空档时间为动画表现时间)")][ShowIf("m_IsShowStockPileData")]
    public List<StockPileStageData> StockPileStataDataList;
    [LabelText("技能引导特效")][ShowIf("m_IsShowSkillGuide")]
    public GameObject GuideEffect;
    [LabelText("技能引导范围")][ShowIf("m_IsShowSkillGuide")]
    public float GuideRange;

    [LabelText("组合技能Id(衔接的下一个技能的Id)")]
    public int CombinationSkillId;
    

    //************************************************技能渲染************************************************
    [TitleGroup("技能渲染", "所有英雄渲染数据会在技能开始释放时触发")]

    [LabelText("技能攻击特效")]
    public GameObject HitEffect;
    [LabelText("攻击特效存活时间(单位ms)")]
    public int HitEffectSurvivalTimeMS = 100;
    [LabelText("技能命中音效")]
    public AudioClip HitAudio;
    [LabelText("是否显示技能立绘")]
    public bool isShowPortrait;
    [LabelText("技能立绘")][ShowIf("isShowPortrait")]
    public GameObject PortraitObj;
    [LabelText("技能描述")]
    public string Describe;

    public void OnSkillTypeChange(SkillType type) {
        m_IsShowStockPileData = type == SkillType.StockPile;
        m_IsShowSkillGuide = type == SkillType.PosGuide;
    }

}

/// <summary>
/// 蓄力阶段的数据
/// </summary>
[System.Serializable]
public class StockPileStageData
{
    [LabelText("蓄力阶段")]
    public int Stage;

    [LabelText("当前蓄力阶段要触发的技能ID")]
    public int SkillId;

    [LabelText("当前阶段触发的时间")]
    public int StartTimeMS;

    [LabelText("当前阶段结束的时间")]
    public int EndTimeMS;

}

/// <summary>
/// 技能类型
/// </summary>
public enum SkillType
{
    [LabelText("普通的瞬发技能")] Normal = 0,
    [LabelText("吟唱型技能")] Chnat,
    [LabelText("弹道型技能")] Ballistic,
    [LabelText("蓄力型技能")] StockPile,
    [LabelText("位置引导型技能")] PosGuide,
}
