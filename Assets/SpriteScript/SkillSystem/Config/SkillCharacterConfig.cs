using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 绘制技能编辑器中的Character区域
/// </summary>
[HideMonoScript]
[System.Serializable]
public class SkillCharacterConfig
{
    //临时的角色模型
    private GameObject m_TempCharacter;
    //是否正在播放
    private bool m_IsPlaying = false;
    //上次运行的时间
    private double m_LastRunTime = 0;
    //当前模型身上的动画组件
    private Animation m_TempAnimation;

    //************************************************角色模型************************************************
    [AssetList][LabelText("角色模型")][PreviewField(70, ObjectFieldAlignment.Center)]
    public GameObject SkillCharacter;

    //************************************************技能动画数据************************************************
    [TitleGroup("技能渲染", "所有英雄渲染数据会在技能开始释放时触发")]
    [LabelText("技能动画片段")]
    public AnimationClip SkillAnimClip;

    [BoxGroup("动画数据")][ProgressBar(0, 100, r: 0, g: 255, b: 0, Height = 25)][HideLabel]
    [OnValueChanged("OnAnimProgressValueChanged")]
    public byte AnimProgress = 0;

    [BoxGroup("动画数据")][LabelText("是否循环动画")]
    public bool IsLoopAnim = false;

    [BoxGroup("动画数据")][LabelText("动画循环次数")][ShowIf("IsLoopAnim")]
    public int AnimLoopCount;

    [BoxGroup("动画数据")][LabelText("逻辑帧")]
    public int LogicFrame = 0;

    [BoxGroup("动画数据")][LabelText("动画长度")]
    public float AnimLength = 0;

    [BoxGroup("动画数据")][LabelText("技能持续时间(毫秒ms)")]
    public float SkillDurating = 0;

    [ButtonGroup("按钮数组")]
    [Button("播放", ButtonSizes.Large)][GUIColor(0.4f, 0.8f, 1)]
    public void Play() {
        if(SkillCharacter != null) {
            //先从场景中寻找技能对象,如果查找不到,就主动克隆一个
            string name = SkillCharacter.name;
            m_TempCharacter = GameObject.Find(name);
            if(m_TempCharacter == null) {
                m_TempCharacter = GameObject.Instantiate(SkillCharacter);
            }

            //判断模型身上是否有该动画,如果没有则进行添加
            m_TempAnimation = m_TempCharacter.GetComponent<Animation>();
            if (!m_TempAnimation.GetClip(SkillAnimClip.name)) {
                m_TempAnimation.AddClip(SkillAnimClip, SkillAnimClip.name);
            }
            m_TempAnimation.clip = SkillAnimClip;
            //计算动画长度
            AnimLength = IsLoopAnim ? AnimLoopCount * SkillAnimClip.length : SkillAnimClip.length;
            //计算逻辑帧长度(个数) 0.066f是15帧情况下的值
            LogicFrame = (int)(IsLoopAnim ? SkillAnimClip.length / LogicFrameConfig.LogicFrameInterval * AnimLoopCount : SkillAnimClip.length / LogicFrameConfig.LogicFrameInterval);
            //计算技能的持续时间(等同于动画长度,只是单位换算成了毫秒)
            SkillDurating = (int)(IsLoopAnim ? (SkillAnimClip.length * AnimLoopCount) * 1000 : SkillAnimClip.length * 1000);

            m_LastRunTime = 0;
            m_IsPlaying = true;


            SkillEditorWindow window = EditorWindow.GetWindow<SkillEditorWindow>();
            window?.StartPlaySkill();
        }
    }

    [ButtonGroup("按钮数组")]
    [Button("暂停", ButtonSizes.Large)]
    public void Pause() {
        m_IsPlaying = false;
        EditorWindow.GetWindow<SkillEditorWindow>().PausePlaySkill();
    }

    [ButtonGroup("按钮数组")]
    [Button("保存设置", ButtonSizes.Large)][GUIColor(0,1,0)]
    public void SaveSetting() {

    }

    public void OnUpdate(System.Action progressCallback = null) {
        if (m_IsPlaying) {
            if(m_LastRunTime == 0) {
                m_LastRunTime = EditorApplication.timeSinceStartup;
            }
            //获取当前运行时间
            double curRunTime = EditorApplication.timeSinceStartup - m_LastRunTime;
            //计算动画播放进度
            float curAnimNormalization = (float)curRunTime / AnimLength;
            AnimProgress = (byte)Mathf.Clamp(curAnimNormalization * 100, 0, 100);
            //计算逻辑帧
            LogicFrame = (int)(curRunTime / LogicFrameConfig.LogicFrameInterval);
            //动画采样
            m_TempAnimation.clip.SampleAnimation(m_TempCharacter, (float)curRunTime);

            //动画播放完成
            if(AnimProgress == 100) {
                m_IsPlaying = false;
                SkillEditorWindow.GetSkillEditorWindow()?.EndPlaySkill();
            }

            //触发窗口聚焦回调,刷新窗口
            progressCallback?.Invoke();
        }
    }

    /// <summary>
    /// 动画进度条发生变化时的回调函数
    /// </summary>
    public void OnAnimProgressValueChanged(float progress) {
        //先从场景中寻找技能对象,如果查找不到,就主动克隆一个
        string name = SkillCharacter.name;
        m_TempCharacter = GameObject.Find(name);
        if (m_TempCharacter == null) {
            m_TempCharacter = GameObject.Instantiate(SkillCharacter);
        }
        //判断模型身上是否有该动画,如果没有则进行添加
        m_TempAnimation = m_TempCharacter.GetComponent<Animation>();
        //采样动画 进行动画播放
        float progressValue = (progress / 100) * SkillAnimClip.length;
        LogicFrame = (int)(progressValue / LogicFrameConfig.LogicFrameInterval);
        m_TempAnimation.clip.SampleAnimation(m_TempCharacter, progressValue);
    }

}
