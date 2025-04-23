using UnityEditor;
using UnityEngine;

namespace Preference.Editor.Project
{
    public sealed class Striping
    {
        public static void OnGUI(string guid, Rect selectionRect)
        {
            if (Event.current.type != EventType.Repaint) return;

            var t = 1 - (Mathf.PingPong(selectionRect.y, 16f) / 16f);

            var color = new Color(
                EditorGUIUtility.isProSkin ? 1f : 0f,
                EditorGUIUtility.isProSkin ? 1f : 0f,
                EditorGUIUtility.isProSkin ? 1f : 0f,
                EditorGUIUtility.isProSkin ? 0.033f * t : 0.05f * t);

            selectionRect.x += selectionRect.width;
            selectionRect.width = selectionRect.x;
            selectionRect.x -= selectionRect.width;

            EditorGUI.DrawRect(selectionRect, color);
        }
    }
}