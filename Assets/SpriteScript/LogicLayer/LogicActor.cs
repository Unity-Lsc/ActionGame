using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 逻辑演员
/// </summary>
public partial class LogicActor : LogicObject
{

    public override void OnInit() {
        base.OnInit();
        InitActorSkill();
    }

    public override void OnLogicFrameUpdate() {
        base.OnLogicFrameUpdate();
        OnLogicFrameUpdateMove();
        OnLogicFrameUpdateSkill();
        OnLogicFrameUpdateGravity();
    }

    public void PlayAnim(AnimationClip clip) {
        RenderObj.PlayAnim(clip);
    }

    public override void OnRelease() {
        base.OnRelease();
    }

}
