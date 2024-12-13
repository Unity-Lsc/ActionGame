using UnityEngine;

public class SkillEffectRender : RenderObject
{

    public override void OnRelease() {
        base.OnRelease();
        GameObject.Destroy(gameObject);
    }

}
