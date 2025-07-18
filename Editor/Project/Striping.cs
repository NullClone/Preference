using UnityEditor;
using UnityEngine;

namespace Preference.Project
{
    public static class Striping
    {
        public static void OnGUI(string _, Rect selectionRect)
        {
            if (Preference.ProjectStripingFlag == false) return;

            if (Event.current.type == EventType.Repaint)
            {
                Draw(selectionRect);
            }
        }

        public static void Draw(Rect selectionRect)
        {
            if (selectionRect.height == 16)
            {
                var t = 1 - (Mathf.PingPong(selectionRect.y, 16f) / 16f);

                var color = new Color(
                    EditorGUIUtility.isProSkin ? 1f : 0f,
                    EditorGUIUtility.isProSkin ? 1f : 0f,
                    EditorGUIUtility.isProSkin ? 1f : 0f,
                    EditorGUIUtility.isProSkin ? 0.033f * t : 0.05f * t);

                selectionRect.xMin = 16;

                EditorGUI.DrawRect(selectionRect, color);
            }
        }
    }
}