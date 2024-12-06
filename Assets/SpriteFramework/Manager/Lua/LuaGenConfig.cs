using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;
using UnityEngine.Events;

namespace SpriteFramework
{
    public static class LuaGenConfig
    {

        //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
        [LuaCallCSharp]
        public static List<Type> LuaCallCSharp = new List<Type>() {
            typeof(Toggle),
            typeof(Toggle.ToggleEvent),
            typeof(Dropdown.DropdownEvent),
            typeof(GameObject),
        };

        //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
        [CSharpCallLua]
        public static List<Type> CSharpCallLua = new List<Type>() {
            typeof(UnityAction<bool>),
            typeof(BaseAction<int,GameObject>),
        };

    }
}
