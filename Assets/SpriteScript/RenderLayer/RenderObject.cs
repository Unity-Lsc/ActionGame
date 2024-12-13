using UnityEngine;

/// <summary>
/// 渲染对象
/// </summary>
public class RenderObject : MonoBehaviour
{
    /// <summary>
    /// 逻辑对象
    /// </summary>
    public LogicObject LogicObj;

    //插值速度
    protected float m_SmoothSpeed = 10;

    private Vector2 m_RenderDir;
    private Vector3 m_Scale;

    /// <summary>
    /// 渲染层初始化
    /// </summary>
    public virtual void OnInit() { }

    /// <summary>
    /// 渲染层释放
    /// </summary>
    public virtual void OnRelease() { }

    /// <summary>
    /// 设置逻辑对象
    /// </summary>
    public void SetLogicObject(LogicObject obj) {
        LogicObj = obj;
        //初始化位置
        transform.position = LogicObj.LogicPos.ToVector3();
    }

    /// <summary>
    /// Unity引擎渲染帧 根据程序配置,一般为30帧,60帧或者120帧
    /// </summary>
    protected virtual void Update()
    {
        UpdatePosition();
        UpdateDir();
    }

    public virtual void PlayAnim(AnimationClip clip) { }

    /// <summary>
    /// 位置更新逻辑
    /// </summary>
    private void UpdatePosition() {
        //对逻辑位置做插值动画 流畅渲染对象移动
        transform.position = Vector3.Lerp(transform.position, LogicObj.LogicPos.ToVector3(), Time.deltaTime * m_SmoothSpeed);
    }

    /// <summary>
    /// 方向更新逻辑
    /// </summary>
    private void UpdateDir() {
        //transform.rotation = Quaternion.Euler(LogicObj.LogicDir.ToVector3());

        //m_RenderDir.x = LogicObj.LogicXAxis >= 0 ? 0 : -20;
        //m_RenderDir.y = LogicObj.LogicXAxis >= 0 ? 0 : 180;
        //transform.localEulerAngles = m_RenderDir;

        m_Scale.x = LogicObj.LogicXAxis >= 0 ? 1 : -1;
        m_Scale.y = 1;
        transform.localScale = m_Scale;
    }

}
