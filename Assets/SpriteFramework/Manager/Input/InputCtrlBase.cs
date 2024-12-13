using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteFramework
{

    /// <summary>
    /// 触屏方向
    /// </summary>
    public enum TouchDirection
    {
        MoveNone,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight
    }

    /// <summary>
    /// 缩放的类型
    /// </summary>
    public enum ZoomType
    {
        In,//放大
        Out//缩小
    }

    /// <summary>
    /// 输入控制器 基类
    /// </summary>
    public abstract class InputCtrlBase
    {
        /// <summary>
        /// 是否开始拖拽
        /// </summary>
        protected bool m_IsBeginDrag;

        /// <summary>
        /// 是否结束拖拽
        /// </summary>
        protected bool m_IsEndDrag = true;

        /// <summary>
        /// 是否拖拽中
        /// </summary>
        protected bool m_IsDraging;

        /// <summary>
        /// 触屏事件数据
        /// </summary>
        private TouchEventData m_TouchEventData = null;
        /// <summary>
        /// 触屏事件数据
        /// </summary>
        protected TouchEventData TouchEventData {
            get {
                if (m_TouchEventData == null) {
                    m_TouchEventData = new TouchEventData();
                }
                return m_TouchEventData;
            }
        }

        /// <summary>
        /// 滑动方向
        /// </summary>
        protected TouchDirection Direction;

        /// <summary>
        /// 点击回调
        /// </summary>
        protected BaseAction<TouchEventData> OnClick;

        /// <summary>
        /// 开始拖拽
        /// </summary>
        protected BaseAction<TouchEventData> OnBeginDrag;

        /// <summary>
        /// 结束拖拽
        /// </summary>
        protected BaseAction<TouchEventData> OnEndDrag;

        /// <summary>
        /// 拖拽中
        /// </summary>
        protected BaseAction<TouchDirection, TouchEventData> OnDraging;

        /// <summary>
        /// 缩放
        /// </summary>
        protected BaseAction<ZoomType> OnZoom;

        public InputCtrlBase(BaseAction<TouchEventData> onClick, 
            BaseAction<TouchEventData> onBeginDrag,
            BaseAction<TouchEventData> onEndDrag,
            BaseAction<TouchDirection, TouchEventData> onDraging,
            BaseAction<ZoomType> onZoom) {
            OnClick = onClick;
            OnBeginDrag = onBeginDrag;
            OnEndDrag = onEndDrag;
            OnDraging = onDraging;
            OnZoom = onZoom;
        }

        internal abstract void OnUpdate();

        /// <summary>
        /// 点击
        /// </summary>
        /// <returns></returns>
        protected abstract bool Click();

        /// <summary>
        /// 开始拖拽
        /// </summary>
        /// <returns></returns>
        protected abstract bool BeginDrag();

        /// <summary>
        /// 结束拖拽
        /// </summary>
        /// <returns></returns>
        protected abstract bool EndDrag();

        /// <summary>
        /// 拖拽中
        /// </summary>
        /// <returns></returns>
        protected abstract bool Draging();

        /// <summary>
        /// 滑动
        /// </summary>
        /// <param name="touchDirection">滑动方向</param>
        /// <param name="touchEventData">滑动数据</param>
        /// <returns></returns>
        protected abstract bool Move(TouchDirection touchDirection, TouchEventData touchEventData);

        /// <summary>
        /// 缩放
        /// </summary>
        /// <returns></returns>
        protected abstract bool Zoom();

    }
}
