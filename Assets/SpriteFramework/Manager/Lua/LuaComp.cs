using System;
using UnityEngine;

namespace SpriteFramework
{

    /// <summary>
    /// Lua组件类型
    /// </summary>
    public enum LuaCompType
    {
        GameObject = 0,
        Transform,
        Button,
        Image,
        Text,
        RawImage,
        InputField,
        Scrollbar,
        ScrollRect,
        UIMultiScroller,
        UIScroller,
    }

    [Serializable]
    public class LuaCompGroup
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 组件列表
        /// </summary>
        public LuaComp[] Comps;
    }

    /// <summary>
    /// Lua组件
    /// </summary>
    [Serializable]
    public class LuaComp
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 类型
        /// </summary>
        public LuaCompType Type;

        /// <summary>
        /// 组件对应的Transform
        /// </summary>
        public Transform Tran;

    }

}
