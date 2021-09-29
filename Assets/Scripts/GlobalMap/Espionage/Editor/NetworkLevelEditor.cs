using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Text.RegularExpressions;
using System;
using System.Linq;

namespace GlobalMap.Espionage
{
    [CustomPropertyDrawer(typeof(SpyNetworkLevel))]
    public class NetworkLevelEditor : PropertyDrawer
    {
        ReorderableList _list;
		float _lineHeight = EditorGUIUtility.singleLineHeight + 2;
        float _listItemHeight = EditorGUIUtility.singleLineHeight + 5;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var spyPoints = property.FindPropertyRelative("requiredSpyPoints");

            var singleLine = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            //header
            var match = Regex.Match(label.text, @"\d+");
            int networkLevel = Convert.ToInt32(match.Value) + 1;
            EditorGUI.LabelField(singleLine, "Spy Network Level " + networkLevel.ToString(), EditorStyles.boldLabel);
            //number field
            singleLine.y += _lineHeight;
            EditorGUI.PropertyField(singleLine, property.FindPropertyRelative("requiredSpyPoints"));

            //list
            singleLine.y += _lineHeight;
            var actions = property.FindPropertyRelative("activeActions");

            var listRect = new Rect(singleLine.x, singleLine.y, position.width, 50);
            _list = new ReorderableList(actions.serializedObject, actions, true, true, false, false);
            _list.drawElementCallback = ElementCallback;
            _list.drawHeaderCallback = HeaderCallBack;
            _list.DoList(listRect);

            //buttons
            var obj = fieldInfo.GetValue(property.serializedObject.targetObject);
            var actionsList = (obj as SpyNetworkLevel)?.activeActions;

            if (obj.GetType().Name == typeof(List<SpyAction>).Name)
            {
                var index = Convert.ToInt32(Regex.Match(property.propertyPath, @"\d+").Value);
                actionsList = (obj as List<SpyNetworkLevel>)[index]?.activeActions;
            }

            if (actionsList == null) return;

            
            var margin = _listItemHeight * Mathf.Max(actionsList.Count, 1) + 30;
            singleLine.y += margin;
            singleLine.x += (singleLine.width / 2 - 50);
            singleLine.height += 4;
            singleLine.width = 50;

            var style = new GUIStyle(GUI.skin.button);
            style.fontSize += 4;
            style.fontStyle = FontStyle.Bold;

            if (GUI.Button(singleLine, "+", style))
            {
                actionsList.Add(actionsList.LastOrDefault());
            }

            singleLine.x += 50;

            if (GUI.Button(singleLine, "-", style))
            {
                if (actionsList.Count == 0) return;
                actionsList.RemoveAt(actionsList.Count - 1);
            }

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var actions = property.FindPropertyRelative("activeActions");
            int size = Mathf.Max(actions?.arraySize ?? 1, 1);
            return size * _listItemHeight + 5 * _lineHeight;
        }

        void HeaderCallBack(Rect rect)
        {
            EditorGUI.LabelField(rect, "Active Actions");
        }


        void ElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = _list.serializedProperty.GetArrayElementAtIndex(index);
			rect.height -= 4;

            EditorGUI.PropertyField(rect, element);


        }

    }
}