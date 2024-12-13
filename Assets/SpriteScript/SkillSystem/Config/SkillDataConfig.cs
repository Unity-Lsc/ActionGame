using System.Collections.Generic;
using UnityEngine;
using SpriteFramework;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 储存技能配置的数据
/// </summary>
[CreateAssetMenu(fileName = "SkillConfig", menuName = "SkillConfig", order = 0)]
public class SkillDataConfig : ScriptableObject
{
    //角色数据配置
    public SkillCharacterConfig CharacterCfg;

    /// <summary>
    /// 技能基础数据配置
    /// </summary>
    public SkillConfig SkillCfg;

    //技能伤害配置列表
    public List<SkillDamegeConfig> DamageCfgList;

    //技能特效配置列表
    public List<SkillEffectConfig> EffectCfgList;

#if UNITY_EDITOR

    public static void SaveSkillData(SkillCharacterConfig characterCfg, SkillConfig skillCfg, List<SkillDamegeConfig> damageCfgList, List<SkillEffectConfig> effectCfgList) {
        //通过代码创建SkillDataConfig的实例,并对字段进行赋值储存
        SkillDataConfig skillDataConfig = ScriptableObject.CreateInstance<SkillDataConfig>();
        skillDataConfig.CharacterCfg = characterCfg;
        skillDataConfig.SkillCfg = skillCfg;
        skillDataConfig.DamageCfgList = damageCfgList;
        skillDataConfig.EffectCfgList = effectCfgList;

        //把当前实例存储为.asset文件
        FileUtils.CheckDirectoryAndCreateDir(SFConstDefine.SkillConfigDataRoot);
        string assetPath = SFConstDefine.SkillConfigDataRoot + skillCfg.Id + ".asset";
        //如果资源已经存在 先进行删除 再创建
        AssetDatabase.DeleteAsset(assetPath);
        AssetDatabase.CreateAsset(skillDataConfig, assetPath);
    }

    [Button("配置技能", ButtonSizes.Large), GUIColor("green")]
    public void ShowSkillWindow() {
        SkillEditorWindow.ShowWindow().LoadSkillData(this);
    }

#endif

}
