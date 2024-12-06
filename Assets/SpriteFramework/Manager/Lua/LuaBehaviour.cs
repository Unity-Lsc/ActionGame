using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace SpriteFramework
{
    [LuaCallCSharp]
    public class LuaBehaviour : UIFormBase
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

        /// <summary>
        /// Lua脚本执行环境中的表
        /// </summary>
        private LuaTable _scriptEnv;

        /// <summary>
        /// 挂载在这个对象上的Lua脚本
        /// </summary>
        public TextAsset LuaScript;

        [Header("Lua组件分组")]
        [SerializeField]
        private LuaCompGroup[] _luaCompGroups;

        protected override void Awake() {
            base.Awake();
            if (LuaScript == null) return;
            _scriptEnv = LuaManager.LuaEnv.NewTable();

            //为每个脚本设置独立的环境,可在一定程度上防止脚本间全局变量(函数)的冲突
            LuaTable meta = LuaManager.LuaEnv.NewTable();
            meta.Set("__index", LuaManager.LuaEnv.Global);
            _scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            LuaManager.LuaEnv.DoString(LuaScript.text, LuaScript.name, _scriptEnv);
            _onAwake = _scriptEnv.Get<OnAwakeHandler>("Awake");
            _onStart = _scriptEnv.Get<OnStartHandler>("Start");
            _onEnable = _scriptEnv.Get<OnEnableHandler>("OnEnable");
            _onDisable = _scriptEnv.Get<OnDisableHandler>("OnDisable");
            _onDestroy = _scriptEnv.Get<OnDestroyHandler>("OnDestroy");

            _scriptEnv.Set("self", this);
            _scriptEnv.Set("transform", transform);
            _scriptEnv.Set("gameObject", gameObject);
            SetLuaComps();

            _onAwake?.Invoke(transform);

        }


        /// <summary>
        /// 设置UI组件
        /// </summary>
        private void SetLuaComps() {
            int len = _luaCompGroups.Length;
            for (int i = 0; i < len; i++) {
                LuaCompGroup group = _luaCompGroups[i];
                int lenComp = group.Comps.Length;
                for (int j = 0; j < lenComp; j++) {
                    LuaComp comp = group.Comps[j];
                    UnityEngine.Object tempObj = null;
                    tempObj = comp.Type switch {
                        LuaCompType.Transform => comp.Tran,
                        LuaCompType.Button => comp.Tran.GetComponent<Button>(),
                        LuaCompType.Image => comp.Tran.GetComponent<Image>(),
                        LuaCompType.Text => comp.Tran.GetComponent<Text>(),
                        LuaCompType.RawImage => comp.Tran.GetComponent<RawImage>(),
                        LuaCompType.InputField => comp.Tran.GetComponent<InputField>(),
                        LuaCompType.Scrollbar => comp.Tran.GetComponent<Scrollbar>(),
                        LuaCompType.ScrollRect => comp.Tran.GetComponent<ScrollRect>(),
                        LuaCompType.UIMultiScroller => comp.Tran.GetComponent<UIMultiScroller>(),
                        LuaCompType.UIScroller => comp.Tran.GetComponent<UIScroller>(),
                        _ => comp.Tran.gameObject,
                    };
                    _scriptEnv.Set(comp.Name, tempObj);
                }
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

            LuaScript = null;
            if(_scriptEnv != null) {
                _scriptEnv.Dispose();
                _scriptEnv = null;
            }

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
