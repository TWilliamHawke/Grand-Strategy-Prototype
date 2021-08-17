using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

[CustomEditor(typeof(UnitTemplate))]
public class UnitTemplateEditor : Editor
{

    SerializedProperty _nameParts;
    SerializedProperty _unitClass;
    SerializedProperty _canNotEdit;
    GUIStyle _emptySpriteStyle;
    UnitTemplate _template;

    void OnEnable()
    {
        _nameParts = serializedObject.FindProperty("_nameParts");
        _unitClass = serializedObject.FindProperty("unitClass");
        _canNotEdit = serializedObject.FindProperty("_canNotEdit");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        // serializedObject.Update();

        // DrawProperties();

        // serializedObject.ApplyModifiedProperties();

    }

    private void DrawProperties()
    {
        _template = target as UnitTemplate;
        if (_template == null)return;


        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 18;
        style.richText = true;
        style.fixedHeight = 25;

        EditorGUILayout.LabelField(_template.nameParts.GetFullName(), style);
        EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(_nameParts);

        DrawUnitClass();

        style.fontSize = 14;
        GUILayout.Label("Equipment", style);
        EditorGUILayout.BeginVertical(GUI.skin.window);

        DrawEquipment<Weapon>(EquipmentSlots.primaryWeapon);
        DrawEquipment<Weapon>(EquipmentSlots.secondaryWeapon);
        DrawEquipment<Shield>(EquipmentSlots.shield);
        DrawEquipment<ArmourInfo>(EquipmentSlots.armour);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(_canNotEdit);

    }

    private void DrawUnitClass()
    {
        EditorGUILayout.BeginHorizontal(GUI.skin.box);
        EditorGUILayout.BeginVertical();

        EditorGUILayout.PropertyField(_unitClass);
        DrawMeleeSkillSlider();

        EditorGUILayout.EndVertical();

        if (_template.unitClass != null)
        {
            var classSpite = _template.unitClass.defaultIcon;
            GUILayout.Label(classSpite.texture, GUILayout.Width(52), GUILayout.Height(52));
        }
        EditorGUILayout.EndHorizontal();
    }

    void DrawMeleeSkillSlider()
    {
        if(_template.unitClass == null) return;
        _template.meleeSkill = (int)EditorGUILayout.Slider("Melee skill", _template.meleeSkill, 0, _template.unitClass.weaponSkill);
    }

    void DrawEquipment<T>(EquipmentSlots slot) where T : Equipment
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();

        string label = slot.ToString();
        label = SplitLabel(label);
        GUILayout.Label(label);

        _template.FindEquipment<T>(slot, out var equipment);

        T updatedEquipment = (T)EditorGUILayout.ObjectField(equipment, typeof(T), false);

        EditorGUILayout.EndVertical();

        if (updatedEquipment != null)
        {
            _template.AddEquipment(slot, updatedEquipment);
            GUILayout.Label(updatedEquipment.Sprite.texture, GUILayout.Width(52), GUILayout.Height(52));
        }
        else
        {
            GUILayout.Button("empty", GUILayout.Width(52), GUILayout.Height(52));
        }

        EditorGUILayout.EndHorizontal();

    }

    private static string SplitLabel(string label)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(System.Char.ToUpper(label[0]));

        foreach (char c in label.Substring(1))
        {
            if (System.Char.IsUpper(c) && builder.Length > 0) builder.Append(' ');
            builder.Append(c);
        }

        label = builder.ToString();
        return label;
    }
}
