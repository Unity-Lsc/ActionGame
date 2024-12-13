using FixMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//逻辑帧 移动
public partial class LogicActor
{

    private FixIntVector3 m_InputDir;

    /// <summary>
    /// 逻辑帧位置更新
    /// </summary>
    public void OnLogicFrameUpdateMove() {
        if (ActionState != LogicObjectActionState.Idle && ActionState != LogicObjectActionState.Move) return;
        //计算逻辑位置
        LogicPos += m_InputDir * LogicMoveSpeed * (FixInt)LogicFrameConfig.LogicFrameInterval;
        //计算逻辑对象的朝向
        if(LogicDir != m_InputDir) {
            LogicDir = m_InputDir;
        }
        //计算逻辑轴向
        if(LogicDir.x != FixInt.Zero) {
            LogicXAxis = LogicDir.x > 0 ? 1 : -1;
        }
    }

    public void InputLogicFrameEvent(FixIntVector3 inputDir) {
        m_InputDir = inputDir;
    }

}
