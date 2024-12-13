using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpriteFramework
{
    /// <summary>
    /// 输入管理器
    /// </summary>
    public class InputManager
    {

        /// <summary>
        /// 摇杆
        /// </summary>
        public Joystick Joystick;

        /// <summary>
        /// 点击回调
        /// </summary>
        public event BaseAction<TouchEventData> OnClick;

        /// <summary>
        /// 开始拖拽
        /// </summary>
        public event BaseAction<TouchEventData> OnBeginDrag;

        /// <summary>
        /// 结束拖拽
        /// </summary>
        public event BaseAction<TouchEventData> OnEndDrag;

        /// <summary>
        /// 拖拽中
        /// </summary>
        public event BaseAction<TouchDirection, TouchEventData> OnDraging;

        /// <summary>
        /// 缩放
        /// </summary>
        public event BaseAction<ZoomType> OnZoom;

        /// <summary>
        /// 输入控制器
        /// </summary>
        private InputCtrlBase m_InputCtrl;

        private List<RaycastResult> m_RaycastResultList = new List<RaycastResult>();

        public InputManager() {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            m_InputCtrl = new StandalonInputCtrl(
                (TouchEventData t) => { OnClick?.Invoke(t); },
                (TouchEventData t) => { OnBeginDrag?.Invoke(t); },
                (TouchEventData t) => { OnEndDrag?.Invoke(t); },
                (TouchDirection t1, TouchEventData t2) => { OnDraging?.Invoke(t1, t2); },
                (ZoomType t) => { OnZoom?.Invoke(t); }
                );
#else
            //移动端
            m_InputCtrl = new MobileInputCtrl(
                (TouchEventData t) => { OnClick(t); },
                (TouchEventData t) => { OnBeginDrag(t); },
                (TouchEventData t) => { OnEndDrag(t); },
                (TouchDirection t1, TouchEventData t2) => { OnDrag(t1, t2); },
                (ZoomType t) => { OnZoom(t); }
                );
#endif
        }

        internal void OnUpdate() {
            m_InputCtrl.OnUpdate();
        }

        /// <summary>
        /// 判断UI穿透
        /// </summary>
        /// <param name="screenPosition"></param>
        public bool IsPointerOverGameObject(Vector2 screenPosition) {
            //实例化点击事件  
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            //将点击位置的屏幕坐标赋值给点击事件  
            eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

            m_RaycastResultList.Clear();
            //向点击处发射射线  
            EventSystem.current.RaycastAll(eventDataCurrentPosition, m_RaycastResultList);

            return m_RaycastResultList.Count > 0;
        }

        public void Dispose() {
            m_RaycastResultList.Clear();
            m_RaycastResultList = null;
        }

    }
}
