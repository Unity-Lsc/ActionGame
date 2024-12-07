using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 技能编辑器窗口
/// </summary>
public class SkillEditorWindow : OdinEditorWindow
{
    [TabGroup("SkillCharacter", "模型动画数据", SdfIconType.PersonFill, TextColor = "orange")]
    public SkillCharacterConfig Character = new SkillCharacterConfig();

    [TabGroup("SkillEditor", "Skill", SdfIconType.Robot, TextColor = "lightmagenta")]
    public SkillConfig Skill = new SkillConfig();

    [MenuItem("Skill/技能编辑器")]
    public static SkillEditorWindow ShowWindow() {
        var window = GetWindowWithRect<SkillEditorWindow>(new Rect(0, 0, 1000, 600));
        window.titleContent = new GUIContent("技能编辑器窗口");
        return window;
    }

    protected override void OnEnable() {
        base.OnEnable();
        EditorApplication.update += OnEditorUpdate;
    }

    protected override void OnDisable() {
        base.OnDisable();
        EditorApplication.update -= OnEditorUpdate;
    }

    public void OnEditorUpdate() {
        try {
            Character.OnUpdate(() => {
                //刷新当前窗口
                Focus();
            });
        }
        catch (System.Exception e) {
            Debug.Log(e.Message);
        }
    }

}
