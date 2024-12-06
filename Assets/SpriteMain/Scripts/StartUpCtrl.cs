using System;
using System.Collections;
using UnityEngine;

public class StartUpCtrl : MonoBehaviour
{
    [SerializeField]
    [Range(1.5f,5.0f)]
    [Tooltip("目标缩放值")]
    private float m_TargetScale = 1.5f;

    [SerializeField]
    [Range(1.5f,5.0f)]
    [Tooltip("放大的持续时间")]
    private float m_Duration = 1.5f;

    private RectTransform m_RectStartUp;
    private Vector3 m_StartScale;
    private Vector3 m_EndScale;
    private Action m_OnComplete;

    private void Awake() {
        m_RectStartUp = transform.Find("txtStartUp").GetComponent<RectTransform>();
    }


    private void Start() {
        m_StartScale = m_RectStartUp.localScale;
        m_EndScale = new Vector3(m_TargetScale, m_TargetScale, m_TargetScale);

        ScaleImage();
    }

    private void ScaleImage(Action onComplete = null) {
        m_OnComplete = onComplete;
        StartCoroutine(ScaleRoutine());
    }

    private IEnumerator ScaleRoutine() {
        float elapsedTime = 0f;
        while (elapsedTime < m_Duration) {
            m_RectStartUp.localScale = Vector3.Lerp(m_StartScale, m_EndScale, elapsedTime / m_Duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        m_RectStartUp.localScale = m_EndScale;
        m_OnComplete?.Invoke();
    }

}
