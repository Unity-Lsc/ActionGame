public class RoleLogic : LogicActor
{

    public int HeroId { get; private set; }

    public RoleLogic(int roleId, RenderObject renderObj) {
        HeroId = roleId;
        RenderObj = renderObj;
        ObjectType = LogicObjectType.Hero;
    }

}
