using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 绘制技能编辑器中的Skill Effect区域
/// </summary>
[System.Serializable]
public class SkillEffectConfig
{
    [AssetList][LabelText("技能特效")][PreviewField(70, ObjectFieldAlignment.Left)]
    public GameObject Effect;
    [LabelText("触发帧")]
    public int TriggerFrame;
    [LabelText("结束帧")]
    public int EndFrame;
    [LabelText("特效偏移位置")]
    public Vector3 OffsetPos;
    [LabelText("特效位置类型")]
    public EffectPosType EffectPosType;
    [ToggleGroup("IsSetParentType", "是否设置父节点类型")]
    public bool IsSetParentType = false;
    [ToggleGroup("IsSetParentType", "父节点类型")]
    public EffectParentType ParentType;

#if UNITY_EDITOR

    //Editor模式下克隆的技能特效
    private GameObject m_EffctClone;

    //当前逻辑帧
    private int m_CurLogicFrame = 0;

    private AnimationAgent m_AnimationAgent;
    private ParticleAgent m_ParticleAgent;

    /// <summary>
    /// 开始播放技能特效
    /// </summary>
    public void StartPlaySkill() {
        DestroyEffect();
        m_CurLogicFrame = 0;
    }

    /// <summary>
    /// 暂停播放技能特效
    /// </summary>
    public void PausePlaySkill() {
        DestroyEffect();
    }

    /// <summary>
    /// 技能特效播放结束
    /// </summary>
    public void EndPlaySkill() {
        DestroyEffect();
    }

    /// <summary>
    /// 逻辑帧更新
    /// </summary>
    public void OnLogicFrameUpdate() {
        if(m_CurLogicFrame == TriggerFrame) {
            CreateEffect();
        }else if(m_CurLogicFrame == EndFrame) {
            DestroyEffect();
        }
        m_CurLogicFrame++;
    }

    /// <summary>
    /// 创建特效
    /// </summary>
    public void CreateEffect() {
        if(Effect != null) {
            m_EffctClone = GameObject.Instantiate(Effect);
            m_EffctClone.transform.position = SkillEditorWindow.GetCharacterPos();

            //Editor模式下播放动画和特效
            m_AnimationAgent = new AnimationAgent();
            m_AnimationAgent.InitPlay(m_EffctClone.transform);

            m_ParticleAgent = new ParticleAgent();
            m_ParticleAgent.InitPlay(m_EffctClone.transform);
        }
    }

    /// <summary>
    /// 销毁特效
    /// </summary>
    public void DestroyEffect() {
        if(m_EffctClone != null) {
            GameObject.DestroyImmediate(m_EffctClone);
        }
        if(m_AnimationAgent != null) {
            m_AnimationAgent.OnDestroy();
            m_AnimationAgent = null;
        }
        if(m_ParticleAgent != null) {
            m_ParticleAgent.OnDestroy();
            m_ParticleAgent = null;
        }
    }

#endif

}

/// <summary>
/// 技能特效的位置类型
/// </summary>
public enum EffectPosType
{
    [LabelText("跟随角色位置和方向")] FollowPosDir = 0,
    [LabelText("只跟随角色方向")] FollowDir,
    [LabelText("屏幕中心位置")] CenterPos,
    [LabelText("引导位置")] GuidePos,
    [LabelText("跟随特效移动位置")] FollowEffectMovePos,
}

/// <summary>
/// 特效的父节点类型
/// </summary>
public enum EffectParentType
{
    [LabelText("无父节点配置")] None = 0,
    [LabelText("左手")] LeftHand,
    [LabelText("右手")] RightHand,
}
