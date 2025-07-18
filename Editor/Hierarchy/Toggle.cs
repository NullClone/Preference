using UnityEditor;
using UnityEngine;

namespace Preference.Editor.Hierarchy
{
    public static class Toggle
    {
        public static void OnGUI(int instanceID, Rect selectionRect)
        {
            if (Preference.HierarchyToggleFlag == false) return;

            var rect = selectionRect;

            rect.xMin = 0;

            if (rect.Contains(Event.current.mousePosition))
            {
                var gameObject = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

                if (gameObject == null) return;

                selectionRect.x = 32f;
                selectionRect.xMax = 48f;

                var active = EditorGUI.Toggle(selectionRect, gameObject.activeSelf);

                if (gameObject.activeSelf == active) return;

                Undo.RecordObject(gameObject, gameObject.name);

                gameObject.SetActive(active);
            }
        }
    }
}