using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 技能编辑器窗口
/// </summary>
public class SkillEditorWindow : OdinEditorWindow
{
    [TabGroup("SkillCharacter", "模型动画数据", SdfIconType.PersonFill, TextColor = "orange")]
    public SkillCharacterConfig Character = new SkillCharacterConfig();

    [TabGroup("SkillEditor", "Skill", SdfIconType.Robot, TextColor = "lightmagenta")]
    public SkillConfig Skill = new SkillConfig();

    [TabGroup("SkillEditor", "Damage", SdfIconType.At, TextColor = "lightred")]
    public List<SkillDamegeConfig> DamageList = new List<SkillDamegeConfig>();

    [TabGroup("SkillEditor", "Effect", SdfIconType.OpticalAudio, TextColor = "blue")]
    public List<SkillEffectConfig> EffectList = new List<SkillEffectConfig>();


#if UNITY_EDITOR

    //是否在播放技能
    private bool m_IsPlayingSkill = false;

    [MenuItem("Skill/技能编辑器")]
    public static SkillEditorWindow ShowWindow() {
        var window = GetWindowWithRect<SkillEditorWindow>(new Rect(0, 0, 1000, 600));
        window.titleContent = new GUIContent("技能编辑器窗口");
        return window;
    }

    /// <summary>
    /// 获取编辑器下角色的位置
    /// </summary>
    public static Vector3 GetCharacterPos() {
        SkillEditorWindow window = GetWindow<SkillEditorWindow>();
        if(window != null) {
            return window.Character.SkillCharacter.transform.position;
        }
        return Vector3.zero;
    }

    protected override void OnEnable() {
        base.OnEnable();
        EditorApplication.update += OnEditorUpdate;
    }

    protected override void OnDisable() {
        base.OnDisable();
        EditorApplication.update -= OnEditorUpdate;
    }

    /// <summary>
    /// 开始播放列表中的技能特效
    /// </summary>
    public void StartPlaySkill() {
        foreach (var item in EffectList) {
            item.StartPlaySkill();
        }
        m_LogicAccRunTime = 0;
        m_NextLogicFrameTime = 0;
        m_LastUpdateTime = 0;
        m_IsPlayingSkill = true;
    }

    /// <summary>
    /// 暂停播放 列表中的技能特效
    /// </summary>
    public void PausePlaySkill() {
        foreach (var item in EffectList) {
            item.PausePlaySkill();
        }
    }

    /// <summary>
    /// 列表中的技能特效播放结束
    /// </summary>
    public void EndPlaySkill() {
        foreach (var item in EffectList) {
            item.EndPlaySkill();
        }
        m_LogicAccRunTime = 0;
        m_NextLogicFrameTime = 0;
        m_LastUpdateTime = 0;
        m_IsPlayingSkill = false;
    }

    public void OnEditorUpdate() {
        try {
            Character.OnUpdate(() => {
                //刷新当前窗口
                Focus();
            });

            if (m_IsPlayingSkill) {
                OnLogicUpdate();
            }
        }
        catch (System.Exception e) {
            Debug.Log(e.Message);
        }
    }

    //逻辑帧累计运行时间
    private float m_LogicAccRunTime = 0;
    //下一个逻辑帧的时间
    private float m_NextLogicFrameTime = 0;
    //动画缓动时间(当前帧的增量时间)
    private float m_DeltaTime = 0;
    //上次更新的时间
    private double m_LastUpdateTime = 0;

    /// <summary>
    /// 逻辑Update
    /// </summary>
    public void OnLogicUpdate() {
        //模拟帧同步的更新 以0.066秒的间隔进行更新
        if (m_LastUpdateTime == 0) {
            m_LastUpdateTime = EditorApplication.timeSinceStartup;
        }
        //计算逻辑帧运行时间累加
        m_LogicAccRunTime = (float)(EditorApplication.timeSinceStartup - m_LastUpdateTime);
        while (m_LogicAccRunTime > m_NextLogicFrameTime) {
            OnLogicFrameUpdate();
            //计算下一个逻辑帧的时间
            m_NextLogicFrameTime += LogicFrameConfig.LogicFrameInterval;
        }
    }

    private void OnLogicFrameUpdate() {
        foreach (var item in EffectList) {
            item.OnLogicFrameUpdate();
        }
    }

#endif

}
