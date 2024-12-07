using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// ���Ƽ��ܱ༭���е�Character����
/// </summary>
[HideMonoScript]
[System.Serializable]
public class SkillCharacterConfig
{

    private GameObject m_TempCharacter;

    //************************************************��ɫģ��************************************************
    [AssetList]
    [LabelText("��ɫģ��")]
    [PreviewField(70, ObjectFieldAlignment.Center)]
    public GameObject SkillCharacter;

    //************************************************���ܶ�������************************************************
    [TitleGroup("������Ⱦ", "����Ӣ����Ⱦ���ݻ��ڼ��ܿ�ʼ�ͷ�ʱ����")]
    [LabelText("���ܶ���Ƭ��")]
    public AnimationClip SkillAnimClip;

    [BoxGroup("��������")][LabelText("�������Ž���")]
    [ProgressBar(0, 100, r: 0, g: 255, b: 0, Height = 25)]
    public byte AnimProgress = 0;

    [BoxGroup("��������")][LabelText("�Ƿ�ѭ������")]
    public bool IsLoopAnim = false;

    [BoxGroup("��������")][LabelText("����ѭ������")]
    public int AnimLoopCount;

    [BoxGroup("��������")][LabelText("�߼�֡")]
    public int LogicFrame = 0;

    [BoxGroup("��������")][LabelText("��������")]
    public float AnimLength = 0;

    [BoxGroup("��������")][LabelText("���ܳ���ʱ��(����ms)")]
    public float SkillDurating = 0;

    [ButtonGroup("��ť����")]
    [Button("����", ButtonSizes.Large)][GUIColor(0.4f, 0.8f, 1)]
    public void Play() {
        if(SkillCharacter != null) {
            //�ȴӳ�����Ѱ�Ҽ��ܶ���,������Ҳ���,��������¡һ��
            string name = SkillCharacter.name;
            m_TempCharacter = GameObject.Find(name);
            if(m_TempCharacter == null) {
                m_TempCharacter = GameObject.Instantiate(SkillCharacter);
            }

            //�ж�ģ�������Ƿ��иö���,���û����������
            var animation = m_TempCharacter.GetComponent<Animation>();
            if (!animation.GetClip(SkillAnimClip.name)) {
                animation.AddClip(SkillAnimClip, SkillAnimClip.name);
            }
            animation.clip = SkillAnimClip;
            //���㶯������
            AnimLength = IsLoopAnim ? AnimLoopCount * SkillAnimClip.length : SkillAnimClip.length;
            //�����߼�֡����(����) 0.066f��15֡����µ�ֵ
            LogicFrame = (int)(IsLoopAnim ? SkillAnimClip.length / 0.066f * AnimLoopCount : SkillAnimClip.length / 0.066f);
            //���㼼�ܵĳ���ʱ��(��ͬ�ڶ�������,ֻ�ǵ�λ������˺���)
            SkillDurating = (int)(IsLoopAnim ? (SkillAnimClip.length * AnimLoopCount) * 1000 : SkillAnimClip.length * 1000);
        }
    }

    [ButtonGroup("��ť����")]
    [Button("��ͣ", ButtonSizes.Large)]
    public void Pause() {

    }

    [ButtonGroup("��ť����")]
    [Button("��������", ButtonSizes.Large)][GUIColor(0,1,0)]
    public void SaveSetting() {

    }

}
