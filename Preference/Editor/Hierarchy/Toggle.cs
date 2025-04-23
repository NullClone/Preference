using UnityEditor;
using UnityEngine;

namespace Preference.Editor.Hierarchy
{
    public static class Toggle
    {
        public static void OnGUI(int instanceID, Rect selectionRect)
        {
            selectionRect.x += 32f;
            selectionRect.xMin = 0f;

            if (selectionRect.Contains(Event.current.mousePosition))
            {
                var gameObject = (GameObject)EditorUtility.InstanceIDToObject(instanceID);

                if (gameObject == null) return;

                selectionRect.x = selectionRect.xMax - 31f;
                selectionRect.y -= 0.5f;

                var active = EditorGUI.Toggle(selectionRect, gameObject.activeSelf);

                if (gameObject.activeSelf == active) return;

                Undo.RecordObject(gameObject, gameObject.name);

                gameObject.SetActive(active);
            }
        }
    }
}