using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 技能编辑器窗口
/// </summary>
public class SkillEditorWindow : OdinEditorWindow
{
    [TabGroup("Skill", "模型动画数据", SdfIconType.PersonFill, TextColor = "orange")]
    public SkillCharacterConfig Character = new SkillCharacterConfig();

    [MenuItem("Skill/技能编辑器")]
    public static SkillEditorWindow ShowWindow() {
        var window = GetWindowWithRect<SkillEditorWindow>(new Rect(0, 0, 1000, 600));
        window.titleContent = new GUIContent("技能编辑器窗口");
        return window;
    }

}
