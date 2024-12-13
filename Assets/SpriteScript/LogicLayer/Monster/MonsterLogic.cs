using FixIntPhysics;
using FixMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLogic : LogicActor
{

    public int MonsterId { get; private set; }

    public MonsterLogic(int monsterId, RenderObject renderObj, FixIntBoxCollider boxCollider, FixIntVector3 initPos) {
        MonsterId = monsterId;
        RenderObj = renderObj;
        Collider = boxCollider;
        LogicPos = initPos;
        ObjectType = LogicObjectType.Monster;
    }

}
