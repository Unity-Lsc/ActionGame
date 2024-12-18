using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace SpriteFramework
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public class ResManager
    {

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="assetPath">资源路径</param>
        public AssetOperationHandle LoadAssetAsync<T>(string assetPath, string packageName = null) where T : UnityEngine.Object {
            if(packageName == null) {
                var handle = YooAssets.LoadAssetAsync<T>(assetPath);
                return handle;
            }

            var package = YooAssets.TryGetPackage(packageName);
            if(package == null) {
                return null;
            }
            
            return package.LoadAssetAsync<T>(assetPath); 
        }

        /// <summary>
        /// 使用Async await进行加载
        /// </summary>
        public async Task<T> LoadAssetAwait<T>(string assetPath, string packageName = null) where T : UnityEngine.Object {
            if (packageName == null) {
                packageName = SFConstDefine.DefaultPackageName;
            }
            ResourcePackage package = YooAssets.TryGetPackage(packageName);
            if (package == null) {
                return null;
            }

            var handler = package.LoadAssetAsync(assetPath);
            await handler.Task;
            T obj = handler.AssetObject as T;
            handler.Dispose();
            return obj;
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="assetPath">资源路径</param>
        public T LoadAsset<T>(string assetPath, string packageName = null) where T : Object {
            T assetObj = null;
            AssetOperationHandle handle;

            if (packageName == null) {
                handle = YooAssets.LoadAssetSync<T>(assetPath);
            } else {
                var package = YooAssets.TryGetPackage(packageName);
                if (package == null) {
                    return null;
                }
                handle = package.LoadAssetSync<T>(assetPath);
            }

            if (handle != null) {
                assetObj = handle.AssetObject as T;
                handle.Dispose();
            }
            return assetObj;
        }

        #region 封装的具体加载资源的方法

        /// <summary>
        /// 异步加载配置文件
        /// </summary>
        /// <param name="dataTableName">加载的配置文件名字</param>
        public AssetOperationHandle LoadDataTableAsync(string dataTableName, string packageName = null) {
            string dataTablePath = Path.Combine(SFConstDefine.DataTableRoot, dataTableName);
            return LoadAssetAsync<TextAsset>(dataTablePath,packageName);
        }

        /// <summary>
        /// 同步加载配置文件
        /// </summary>
        /// <param name="dataTableName">加载的配置文件名字</param>
        public TextAsset LoadDataTable(string dataTableName, string packageName = null) {
            string dataTablePath = Path.Combine(SFConstDefine.DataTableRoot, dataTableName);
            return LoadAsset<TextAsset>(dataTablePath, packageName);
        }

        /// <summary>
        /// 异步加载音频文件
        /// </summary>
        /// <param name="audioName">加载的声音文件名字</param>
        public AssetOperationHandle LoadSoundAsync(string audioName, string packageName = null) {
            string soundPath = Path.Combine(SFConstDefine.SoundRoot, audioName);
            return LoadAssetAsync<AudioClip>(soundPath, packageName);
        }

        /// <summary>
        /// 同步加载背景音乐
        /// </summary>
        /// <param name="bgmName">加载的背景音乐文件名字</param>
        public AudioClip LoadBgm(string bgmName, string packageName = null) {
            string soundPath = Path.Combine(SFConstDefine.BgmRoot, bgmName);
            return LoadAsset<AudioClip>(soundPath, packageName);
        }

        /// <summary>
        /// 同步加载音效
        /// </summary>
        /// <param name="soundName">加载的声音文件名字</param>
        public AudioClip LoadSound(string soundName, string packageName = null) {
            string soundPath = Path.Combine(SFConstDefine.SoundRoot, soundName);
            return LoadAsset<AudioClip>(soundPath, packageName);
        }

        /// <summary>
        /// 同步加载Lua脚本文件
        /// </summary>
        /// <param name="luaName">Lua的文件名（xLuaLogic下的路径）</param>
        /// <returns></returns>
        public TextAsset LoadLua(string luaName, string packageName = null) {
            string luaPath = SFConstDefine.LuaScriptRoot + luaName;
            return LoadAsset<TextAsset>(luaPath, packageName);
        }

        #endregion 封装的具体加载资源的方法end

        #region Lua中调用的方法

        public void LoadAssetAsync(string assetPath, System.Action<Object> onComplete) {
            var handler = YooAssets.LoadAssetAsync(assetPath, typeof(Object));
            GameEntry.Instance.StartCoroutine(WaitForAsset(handler, onComplete));
        }

        private IEnumerator WaitForAsset(AssetOperationHandle handler, System.Action<Object> onComplete) {
            yield return handler;

            // 检查资源是否加载成功
            if (handler.Status == EOperationStatus.Succeed) {
                Object asset = handler.AssetObject;
                onComplete?.Invoke(asset);
            } else {
                Debug.LogError("Asset loading failed: " + handler.LastError);
            }
            handler.Dispose();
        }

        #endregion Lua中调用的方法end

        /// <summary>
        /// 卸载未使用的资源
        /// </summary>
        public void UnloadUnusedAssets(string packageName = null) {
            packageName ??= SFConstDefine.DefaultPackageName;
            var package = YooAssets.TryGetPackage(packageName);
            if(package == null) {
                return;
            }
            package.UnloadUnusedAssets();
        }

        /// <summary>
        /// 获取路径的最后名称
        /// </summary>
        /// <param name="path"></param>
        public string GetLastPathName(string path) {
            if (path.IndexOf('/') == -1) {
                return path;
            }
            return path.Substring(path.LastIndexOf('/') + 1);
        }

    }
}
