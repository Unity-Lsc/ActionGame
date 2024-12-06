namespace SpriteFramework
{
    /// <summary>
    /// bool变量
    /// </summary>
    public class VarBool : Variable<bool>
    {

        /// <summary>
        /// 分配一个对象
        /// </summary>
        /// <param name="value">初始值</param>
        public static VarBool Alloc(bool value = false) {
            VarBool var = GameEntry.Pool.VarObjectPool.DequeueVarObject<VarBool>();
            var.Value = value;
            var.AddRefCount();
            return var;
        }

        /// <summary>
        /// VarBool -> bool
        /// </summary>
        public static implicit operator bool(VarBool value) {
            return value.Value;
        }

    }
}
