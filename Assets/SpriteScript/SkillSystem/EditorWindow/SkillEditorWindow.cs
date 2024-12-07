using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ���ܱ༭������
/// </summary>
public class SkillEditorWindow : OdinEditorWindow
{
    [TabGroup("Skill", "ģ�Ͷ�������", SdfIconType.PersonFill, TextColor = "orange")]
    public SkillCharacterConfig Character = new SkillCharacterConfig();

    [MenuItem("Skill/���ܱ༭��")]
    public static SkillEditorWindow ShowWindow() {
        var window = GetWindowWithRect<SkillEditorWindow>(new Rect(0, 0, 1000, 600));
        window.titleContent = new GUIContent("���ܱ༭������");
        return window;
    }

}
