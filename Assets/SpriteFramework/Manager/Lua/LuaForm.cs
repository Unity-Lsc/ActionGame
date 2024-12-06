using UnityEngine;
using XLua;
using UnityEngine.UI;

namespace SpriteFramework
{
    /// <summary>
    /// Lua窗口
    /// </summary>
    public class LuaForm : UIFormBase
    {

        public delegate void OnAwakeHandler(Transform transform);
        private OnAwakeHandler _onAwake;

        public delegate void OnStartHandler();
        private OnStartHandler _onStart;

        public delegate void OnEnableHandler();
        private OnEnableHandler _onEnable;

        public delegate void OnDisableHandler();
        private OnDisableHandler _onDisable;

        public delegate void OnDestroyHandler();
        private OnDestroyHandler _onDestroy;

        private LuaTable _scriptEnv;
        private LuaEnv _luaEnv;

        [Header("Lua组件")]
        [SerializeField]
        private LuaCompGroup[] _luaCompGroups;

        protected override void Awake() {
            base.Awake();
            //从LuaManager中获取，全局只有一个
            _luaEnv = LuaManager.LuaEnv;
            if (_luaEnv == null) return;

            _scriptEnv = _luaEnv.NewTable();
            LuaTable meta = _luaEnv.NewTable();
            meta.Set("__index", _luaEnv.Global);
            _scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            string prefabName = name;
            if (prefabName.Contains("(Clone)")) {
                prefabName = prefabName.Split(new string[] { "(Clone)" }, System.StringSplitOptions.RemoveEmptyEntries)[0];
            }

            _onAwake = _scriptEnv.GetInPath<OnAwakeHandler>(prefabName + ".Awake");
            _onStart = _scriptEnv.GetInPath<OnStartHandler>(prefabName + ".Start");
            _onEnable = _scriptEnv.GetInPath<OnEnableHandler>(prefabName + ".OnEnable");
            _onDisable = _scriptEnv.GetInPath<OnDisableHandler>(prefabName + ".OnDisable");
            _onDestroy = _scriptEnv.GetInPath<OnDestroyHandler>(prefabName + ".OnDestroy");

            _scriptEnv.Set("self", this);
            _scriptEnv.Set("transform", transform);
            _scriptEnv.Set("gameObject", gameObject);
            if(_onAwake != null) {
                _onAwake(transform);
            }
        }

        protected override void Start() {
            base.Start();
            _onStart?.Invoke();
        }

        protected override void OnEnable() {
            base.OnEnable();
            _onEnable?.Invoke();
        }

        protected override void OnDisable() {
            base.OnDisable();
            _onDisable?.Invoke();
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            _onDestroy?.Invoke();

            _onAwake = null;
            _onStart = null;
            _onEnable = null;
            _onDisable = null;
            _onDestroy = null;

            //卸载图片资源（不然图片资源的引用会一直存在内存里，没办法通过Resource.UnloadUnuseAsset去进行释放）
            if (_luaCompGroups == null) return;
            int len = _luaCompGroups.Length;
            for (int i = 0; i < len; i++) {
                LuaCompGroup group = _luaCompGroups[i];
                int lenComp = group.Comps.Length;
                for (int j = 0; j < lenComp; j++) {
                    group.Comps[j] = null;
                }
                group = null;
            }
        }

    }
}
