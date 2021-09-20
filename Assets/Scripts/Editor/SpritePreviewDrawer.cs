using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

[CustomPropertyDrawer(typeof(SpritePreviewAttribute))]
public class SpritePreviewDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        // if (PropertyIsSprite(property, out var sprite))
        // {
        //     float previewWidth = (attribute as SpritePreviewAttribute)?.width ?? 0f;
        //     float previewHeight = (attribute as SpritePreviewAttribute)?.height ?? 0f;

        //     var labelRect = new Rect(position.x + previewWidth + 2, position.y + 2, position.width - previewWidth, EditorGUIUtility.singleLineHeight);
        //     EditorGUI.LabelField(labelRect, property.displayName);

        //     labelRect.y += EditorGUIUtility.singleLineHeight;
        //     EditorGUI.PropertyField(labelRect, property, GUIContent.none);

        //     var previewRect = new Rect(position.x, position.y, previewWidth, previewHeight);
        //     EditorGUI.LabelField(previewRect, new GUIContent(sprite.texture));

        // }
        // else
        // {
        // }
            EditorGUI.PropertyField(position, property, label);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // if(PropertyIsSprite(property))
        // {
        //     float height = (attribute as SpritePreviewAttribute)?.height ?? 0f;
        //     var minHeight = EditorGUIUtility.singleLineHeight * 2 + 5;
        //     return Mathf.Max(height, minHeight);
        // }
        // else
        // {
        // }
            return EditorGUIUtility.singleLineHeight;
    }

    static bool PropertyIsSprite(SerializedProperty property)
    {
        return PropertyIsSprite(property, out var _);
    }

    static bool PropertyIsSprite(SerializedProperty property, out Sprite sprite)
    {
        var targetObject = property?.serializedObject?.targetObject;
        Type parentType = targetObject?.GetType();
        var fieldInfo = parentType?.GetField(property?.propertyPath);
        sprite = fieldInfo?.GetValue(targetObject) as Sprite;
        return sprite != null;
    }

}
