using UnityEngine;

/// <summary>
/// UI无限列表的索引
/// </summary>
public class UIScrollIndex : MonoBehaviour
{

    public int Index { get; private set; }

    public void UpdateView(int index, Vector2 pos) {
        Index = index;
        transform.localPosition = pos;
    }

}
