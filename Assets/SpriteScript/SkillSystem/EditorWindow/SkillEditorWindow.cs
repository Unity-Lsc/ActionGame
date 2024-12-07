using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ���ܱ༭������
/// </summary>
public class SkillEditorWindow : OdinEditorWindow
{
    [TabGroup("SkillCharacter", "ģ�Ͷ�������", SdfIconType.PersonFill, TextColor = "orange")]
    public SkillCharacterConfig Character = new SkillCharacterConfig();

    [TabGroup("SkillEditor", "Skill", SdfIconType.Robot, TextColor = "lightmagenta")]
    public SkillConfig Skill = new SkillConfig();

    [MenuItem("Skill/���ܱ༭��")]
    public static SkillEditorWindow ShowWindow() {
        var window = GetWindowWithRect<SkillEditorWindow>(new Rect(0, 0, 1000, 600));
        window.titleContent = new GUIContent("���ܱ༭������");
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
                //ˢ�µ�ǰ����
                Focus();
            });
        }
        catch (System.Exception e) {
            Debug.Log(e.Message);
        }
    }

}
