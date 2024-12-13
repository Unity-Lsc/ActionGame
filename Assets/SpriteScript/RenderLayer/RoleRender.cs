using FixMath;
using SpriteFramework;
using UnityEngine;

/// <summary>
/// 角色渲染对象
/// </summary>
public class RoleRender : RenderObject
{

    private RoleLogic m_RoleLogic;

    private Vector2 m_InpouDir;

    private Animation m_Anim;

    public override void OnInit() {
        base.OnInit();
        m_Anim = transform.GetComponent<Animation>();
        m_RoleLogic = LogicObj as RoleLogic;
        GameEntry.Input.Joystick.OnChanged += OnJoystickMove;
        GameEntry.Input.Joystick.OnUp += OnJoystickUp;
    }

    private void OnJoystickMove(Vector2 pos) {
        m_InpouDir = pos;
        FixIntVector3 logicDir = FixIntVector3.zero;
        if(pos != Vector2.zero) {
            logicDir.x = pos.x;
            logicDir.y = pos.y;
        }
        //向逻辑层传递操作帧数据
        m_RoleLogic.InputLogicFrameEvent(logicDir);
    }

    private void OnJoystickUp(Vector2 pos) {
        m_InpouDir = Vector2.zero;
        //向逻辑层传递操作帧数据
        m_RoleLogic.InputLogicFrameEvent(FixIntVector3.zero);
    }

    protected override void Update() {
        base.Update();
        if(m_RoleLogic.ActionState == LogicObjectActionState.Idle || m_RoleLogic.ActionState == LogicObjectActionState.Move) {
            if (m_InpouDir == Vector2.zero) {
                PlayAnim("Anim_Idle02");
            }
            else {
                PlayAnim("Anim_Run");
            }
        }
    }

    private void PlayAnim(string aniName) {
        m_Anim.CrossFade(aniName, 0.2f);
    }

    /// <summary>
    /// 通过动画文件播放动画
    /// </summary>
    /// <param name="clip">动画文件</param>
    public override void PlayAnim(AnimationClip clip) {
        base.PlayAnim(clip);
        if (m_Anim.GetClip(clip.name) == null) {
            m_Anim.AddClip(clip, clip.name);
        }
        m_Anim.clip = clip;
        PlayAnim(clip.name);
    }

    public override void OnRelease() {
        base.OnRelease();
        GameEntry.Input.Joystick.OnChanged -= OnJoystickMove;
        GameEntry.Input.Joystick.OnUp -= OnJoystickUp;
    }

}
