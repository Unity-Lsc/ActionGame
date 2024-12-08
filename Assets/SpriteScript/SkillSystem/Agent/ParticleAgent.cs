using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 特效代理
/// </summary>
public class ParticleAgent
{

#if UNITY_EDITOR

    //物体上所有的特效集合
    private ParticleSystem[] m_ParticleArr;

    //上次运行的时间
    private double m_LastRunTime = 0;

    public void InitPlay(Transform tran) {
        m_ParticleArr = tran.GetComponentsInChildren<ParticleSystem>();
        EditorApplication.update += OnUpdate;
    }

    public void OnDestroy() {
        EditorApplication.update -= OnUpdate;
    }

    public void OnUpdate() {
        if (m_LastRunTime == 0) {
            m_LastRunTime = EditorApplication.timeSinceStartup;
        }
        //获取当前运行时间
        double curRunTime = EditorApplication.timeSinceStartup - m_LastRunTime;
        if(m_ParticleArr != null && m_ParticleArr.Length > 0) {
            foreach (var item in m_ParticleArr) {
                if(item != null) {
                    //停止所有粒子动效的播放
                    item.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    //关闭由随机种子播放的粒子特效
                    item.useAutoRandomSeed = false;
                    //模拟粒子动效的播放
                    item.Simulate((float)curRunTime);
                }
            }
        }
    }
#endif

}
