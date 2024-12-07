namespace SpriteFramework
{
    /// <summary>
    /// 预加载流程
    /// </summary>
    public class ProcedurePreload : ProcedureBase
    {

        public override void OnEnter() {
            base.OnEnter();
            //加载数据表
            GameEntry.DataTable.LoadDataTable();

            GameEntry.Procedure.ChangeState(ProcedureState.SelectRole);
        }

        public override void OnUpdate() {
            base.OnUpdate();
        }

        public override void OnLeave() {
            base.OnLeave();
        }

        public override void OnDestroy() {
            base.OnDestroy();
        }

    }
}
