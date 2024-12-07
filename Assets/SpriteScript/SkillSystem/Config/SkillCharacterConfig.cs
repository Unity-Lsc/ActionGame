using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// 绘制技能编辑器中的Character区域
/// </summary>
[HideMonoScript]
[System.Serializable]
public class SkillCharacterConfig
{

    private GameObject m_TempCharacter;

    //************************************************角色模型************************************************
    [AssetList]
    [LabelText("角色模型")]
    [PreviewField(70, ObjectFieldAlignment.Center)]
    public GameObject SkillCharacter;

    //************************************************技能动画数据************************************************
    [TitleGroup("技能渲染", "所有英雄渲染数据会在技能开始释放时触发")]
    [LabelText("技能动画片段")]
    public AnimationClip SkillAnimClip;

    [BoxGroup("动画数据")][LabelText("动画播放进度")]
    [ProgressBar(0, 100, r: 0, g: 255, b: 0, Height = 25)]
    public byte AnimProgress = 0;

    [BoxGroup("动画数据")][LabelText("是否循环动画")]
    public bool IsLoopAnim = false;

    [BoxGroup("动画数据")][LabelText("动画循环次数")]
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
            var animation = m_TempCharacter.GetComponent<Animation>();
            if (!animation.GetClip(SkillAnimClip.name)) {
                animation.AddClip(SkillAnimClip, SkillAnimClip.name);
            }
            animation.clip = SkillAnimClip;
            //计算动画长度
            AnimLength = IsLoopAnim ? AnimLoopCount * SkillAnimClip.length : SkillAnimClip.length;
            //计算逻辑帧长度(个数) 0.066f是15帧情况下的值
            LogicFrame = (int)(IsLoopAnim ? SkillAnimClip.length / 0.066f * AnimLoopCount : SkillAnimClip.length / 0.066f);
            //计算技能的持续时间(等同于动画长度,只是单位换算成了毫秒)
            SkillDurating = (int)(IsLoopAnim ? (SkillAnimClip.length * AnimLoopCount) * 1000 : SkillAnimClip.length * 1000);
        }
    }

    [ButtonGroup("按钮数组")]
    [Button("暂停", ButtonSizes.Large)]
    public void Pause() {

    }

    [ButtonGroup("按钮数组")]
    [Button("保存设置", ButtonSizes.Large)][GUIColor(0,1,0)]
    public void SaveSetting() {

    }

}
