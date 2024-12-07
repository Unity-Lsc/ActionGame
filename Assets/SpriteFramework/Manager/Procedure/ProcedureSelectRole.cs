namespace SpriteFramework
{
    /// <summary>
    /// ѡ���ɫ����
    /// </summary>
    public class ProcedureSelectRole : ProcedureBase
    {
        public override void OnEnter()
        {
            base.OnEnter();
            GameEntry.Scene.LoadSceneAsync("SelectRole", () => {
                GameEntry.UI.OpenUIFormLua("UICreateRoleForm");
            });
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnLeave()
        {
            base.OnLeave();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

    }
}
