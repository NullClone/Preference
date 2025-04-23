using UnityEditor;
using UnityEngine;

namespace Preference.Editor.Hierarchy
{
    public sealed class Striping
    {
        public static void OnGUI(int instanceID, Rect selectionRect)
        {
            if (Event.current.type != EventType.Repaint) return;

            var t = Mathf.PingPong(selectionRect.y, 16f) / 16f;

            var color = new Color(
                EditorGUIUtility.isProSkin ? 1f : 0f,
                EditorGUIUtility.isProSkin ? 1f : 0f,
                EditorGUIUtility.isProSkin ? 1f : 0f,
                EditorGUIUtility.isProSkin ? 0.033f * t : 0.05f * t);

            selectionRect.xMin = 32;

            EditorGUI.DrawRect(selectionRect, color);
        }
    }
}