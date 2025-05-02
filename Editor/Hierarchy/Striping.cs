using UnityEditor;
using UnityEngine;

namespace Preference.Editor.Hierarchy
{
    public static class Striping
    {
        public static void OnGUI(int instanceID, Rect selectionRect)
        {
            if (Preference.Flag == false) return;

            if (Event.current.type == EventType.Repaint)
            {
                selectionRect.xMin = 32;

                if (selectionRect.Contains(Event.current.mousePosition)) return;

                var t = Mathf.PingPong(selectionRect.y, 16f) / 16f;

                var color = new Color(
                    EditorGUIUtility.isProSkin ? 1f : 0f,
                    EditorGUIUtility.isProSkin ? 1f : 0f,
                    EditorGUIUtility.isProSkin ? 1f : 0f,
                    EditorGUIUtility.isProSkin ? 0.033f * t : 0.05f * t);

                EditorGUI.DrawRect(selectionRect, color);
            }
        }
    }
}