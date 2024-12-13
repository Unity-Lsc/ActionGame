using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixMath;
using FixIntPhysics;

/// <summary>
/// 逻辑对象的状态
/// </summary>
public enum LogicObjectState
{
    Alive,//存活中
    Death,//死亡
}

/// <summary>
/// 逻辑对象的类型
/// </summary>
public enum LogicObjectType
{
    Hero,
    Monster,
    Effect,
}

/// <summary>
/// 逻辑对象的动作状态
/// </summary>
public enum LogicObjectActionState
{
    Idle,//待机
    Move,//移动中
    Skilling,//释放技能中
    Floating,//浮空中
    Hitting,//受击中
    StockPileing,//蓄力中
}

/// <summary>
/// 逻辑对象(代表角色和怪物同时具有的基础属性)
/// </summary>
public abstract class LogicObject
{
    //逻辑位置
    private FixIntVector3 m_LogicPos;
    public FixIntVector3 LogicPos { get { return m_LogicPos; } set { m_LogicPos = value; } }

    //逻辑朝向
    private FixIntVector3 m_LogicDir;
    public FixIntVector3 LogicDir { get { return m_LogicDir; } set { m_LogicDir = value; } }

    //逻辑旋转角度
    private FixIntVector3 m_LogicAngle;
    public FixIntVector3 LogicAngle { get { return m_LogicAngle; } set { m_LogicAngle = value; } }

    //逻辑移动速度
    private FixInt m_LogicMoveSpeed = 3;
    public FixInt LogicMoveSpeed { get { return m_LogicMoveSpeed; } set { m_LogicMoveSpeed = value; } }

    //逻辑轴向
    private FixInt m_LogicXAxis;
    public FixInt LogicXAxis { get { return m_LogicXAxis; } set { m_LogicXAxis = value; } }

    //当前逻辑对象是否处于激活状态
    private bool m_IsActive;
    public bool IsActive { get { return m_IsActive; } set { m_IsActive = value; } }

    /// <summary>
    /// 渲染对象
    /// </summary>
    public RenderObject RenderObj { get; protected set; }

    /// <summary>
    /// 定点数碰撞体
    /// </summary>
    public FixIntBoxCollider Collider { get; protected set; }

    /// <summary>
    /// 逻辑对象状态
    /// </summary>
    public LogicObjectState ObjectState { get; set; }

    /// <summary>
    /// 逻辑对象的类型
    /// </summary>
    public LogicObjectType ObjectType { get; set; }

    /// <summary>
    /// 逻辑对象动作状态
    /// </summary>
    public LogicObjectActionState ActionState { get; set; }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void OnInit() { }

    /// <summary>
    /// 逻辑帧更新
    /// </summary>
    public virtual void OnLogicFrameUpdate() { }

    /// <summary>
    /// 逻辑对象释放
    /// </summary>
    public virtual void OnRelease() { }

}
