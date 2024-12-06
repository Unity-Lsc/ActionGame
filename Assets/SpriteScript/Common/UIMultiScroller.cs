using SpriteFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI无限列表, 支持n*n
/// </summary>
public class UIMultiScroller : MonoBehaviour
{

    public enum Arrangement
    {
        Horizontal,
        Vertical,
    }

    /// <summary>
    /// 滚动方向
    /// </summary>
    [SerializeField]
    private Arrangement _movement = Arrangement.Vertical;

    /// <summary>
    /// 单列或者单行的Item数量
    /// </summary>
    [SerializeField]
    [Range(1,20)]
    [Tooltip("单列或者单行的Item数量")]
    private int _maxPerLine = 5;

    /// <summary>
    /// 行间距
    /// </summary>
    [SerializeField]
    [Range(0,100)]
    [Tooltip("行间距")]
    private int _spacingX = 5;

    /// <summary>
    /// 列间距
    /// </summary>
    [SerializeField]
    [Range(1,100)]
    [Tooltip("列间距")]
    private int _spacingY = 5;

    /// <summary>
    /// Item的预制体
    /// </summary>
    [SerializeField]
    private RectTransform _itemPrefab;

    /// <summary>
    /// 同屏加载的行数(一般比可显示的行数大2~3行)
    /// </summary>
    private int _viewCount = 6;

    //Item的宽高
    private float _cellWidth;
    private float _cellHeight;

    private ScrollRect _scrollRect;
    private RectTransform _content;

    private int _index = -1;
    private List<UIScrollIndex> _itemList;
    private Queue<UIScrollIndex> _unusedQueue;

    public BaseAction<int, GameObject> OnItemCreate;

    private int _dataCount;
    public int DataCount {
        get { return _dataCount; }
        set {
            _dataCount = value;
            UpdateTotalWidth();
        }
    }

    private void Start() {
        _itemList = new List<UIScrollIndex>();
        _unusedQueue = new Queue<UIScrollIndex>();

        _scrollRect = GetComponent<ScrollRect>();
        _content = _scrollRect.content;
        _scrollRect.horizontal = (_movement == Arrangement.Horizontal);
        _scrollRect.vertical = (_movement == Arrangement.Vertical);

        _cellWidth = _itemPrefab.rect.width;
        _cellHeight = _itemPrefab.rect.height;

        //_itemPrefab.gameObject.SetActive(false);

        if(_movement == Arrangement.Horizontal) {
            _viewCount = Mathf.CeilToInt(_scrollRect.GetComponent<RectTransform>().rect.width / _cellWidth * 1.2f);
        } else {
            _viewCount = Mathf.CeilToInt(_scrollRect.GetComponent<RectTransform>().rect.height / _cellHeight * 1.2f);
        }
        _scrollRect.onValueChanged.AddListener(OnValueChanged);
        OnValueChanged(Vector2.zero);
    }

    /// <summary>
    /// 重置滚动列表
    /// </summary>
    public void ResetScroller() {
        _index = -1;
        UIScrollIndex[] arr = _content.GetComponentsInChildren<UIScrollIndex>();
        for (int i = 0; i < arr.Length; i++) {
            DestroyImmediate(arr[i].gameObject);
        }

        _itemList?.Clear();
        _unusedQueue?.Clear();
        _content.anchoredPosition = new Vector2(0, 1f);
        OnValueChanged(Vector2.zero);
    }

    private void OnValueChanged(Vector2 pos) {
        if (_itemList == null) return;

        int index = GetPosIndex();

        if(_index != index && index > -1) {
            _index = index;
            for(int i = _itemList.Count; i > 0; i--) {
                var item = _itemList[i - 1];
                if(item.Index < index * _maxPerLine || (item.Index >= (index + _viewCount) * _maxPerLine)) {
                    _itemList.Remove(item);
                    _unusedQueue.Enqueue(item);
                }
            }
            for(int i = _index * _maxPerLine; i < (_index + _viewCount) * _maxPerLine; i++) {
                if (i < 0) continue;
                if (i > _dataCount - 1) continue;
                bool isOk = false;
                foreach(var item in _itemList) {
                    if (item.Index == i) isOk = true;
                }
                if (isOk) continue;
                CreateItem(i);
            }
        }

    }

    private void CreateItem(int index) {
        UIScrollIndex item;
        if(_unusedQueue.Count > 0) {
            item = _unusedQueue.Dequeue();
        } else {
            item = Instantiate(_itemPrefab.gameObject, _content).AddComponent<UIScrollIndex>();
        }
        item.UpdateView(index, GetPosition(index));
        _itemList.Add(item);
        OnItemCreate?.Invoke(index, item.gameObject);
    }

    /// <summary>
    /// 获取最上位置的索引
    /// </summary>
    private int GetPosIndex() {
        switch (_movement) {
            case Arrangement.Horizontal:
                return Mathf.FloorToInt(_content.anchoredPosition.x / -(_cellWidth + _spacingX));
            default:
            case Arrangement.Vertical:
                return Mathf.FloorToInt(_content.anchoredPosition.y / (_cellHeight + _spacingY));
        }
    }

    /// <summary>
    /// 根据索引号 获取当前item的位置
    /// </summary>
    private Vector2 GetPosition(int i) {
        switch (_movement) {
            case Arrangement.Horizontal:
                return new Vector2(_cellWidth * (i / _maxPerLine), -(_cellHeight + _spacingY) * (i % _maxPerLine));
            default:
            case Arrangement.Vertical:
                return new Vector2((_cellWidth + _spacingX) * (i % _maxPerLine), -(_cellHeight + _spacingY) * (i / _maxPerLine));
        }
    }

    /// <summary>
    /// 刷新列表的总宽度或者总高度
    /// </summary>
    private void UpdateTotalWidth() {
        int lineCount = Mathf.CeilToInt((float)_dataCount / _maxPerLine);
        switch (_movement) {
            case Arrangement.Horizontal:
                _content.sizeDelta = new Vector2(_cellWidth * lineCount + _spacingX * (lineCount - 1), _content.sizeDelta.y);
                break;
            case Arrangement.Vertical:
                _content.sizeDelta = new Vector2(_content.sizeDelta.x, _cellHeight * lineCount + _spacingY * (lineCount - 1));
                break;
        }
    }

    private void OnDestroy() {
        _itemPrefab = null;
        _content = null;

        _itemList?.Clear();
        _itemList = null;
        _unusedQueue?.Clear();
        _unusedQueue = null;

        OnItemCreate = null;
    }

}
