using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 动画代理
/// </summary>
public class AnimationAgent
{
#if UNITY_EDITOR
    //动画组件
    private Animation m_Anim;

    //上次运行的时间
    private double m_LastRunTime = 0;

    public void InitPlay(Transform tran) {
        m_Anim = tran.GetComponentInChildren<Animation>();
        EditorApplication.update += OnUpdate;
    }

    public void OnDestroy() {
        EditorApplication.update -= OnUpdate;
    }

    public void OnUpdate() {
        if (m_Anim == null || m_Anim.clip == null) return;
        if (m_LastRunTime == 0) {
            m_LastRunTime = EditorApplication.timeSinceStartup;
        }
        //获取当前运行时间
        double curRunTime = EditorApplication.timeSinceStartup - m_LastRunTime;
        //动画采样
        m_Anim.clip.SampleAnimation(m_Anim.gameObject, (float)curRunTime);
    }
#endif
}
