using FixMath;

public class SkillEffectLogic : LogicObject
{

    private LogicActor m_SkillActor;

    private SkillEffectConfig m_EffectConfig;

    public SkillEffectLogic(LogicObjectType objectType, SkillEffectConfig effectConfig, RenderObject renderObject, LogicActor skillCreator) {
        this.ObjectType = objectType;
        this.RenderObj = renderObject;
        m_SkillActor = skillCreator;
        m_EffectConfig = effectConfig;
        this.LogicXAxis = skillCreator.LogicXAxis;

        //初始化特效的逻辑位置
        if(effectConfig.EffectPosType == EffectPosType.FollowDir || effectConfig.EffectPosType == EffectPosType.FollowPosDir) {
            FixIntVector3 offset = new FixIntVector3(effectConfig.OffsetPos) * LogicXAxis;
            offset.y = FixIntMath.Abs(offset.y);
            LogicPos = skillCreator.LogicPos + offset;
        }

    }

    public override void OnRelease() {
        base.OnRelease();
        RenderObj.OnRelease();
    }

}
