using UnityEditor;
using UnityEngine;

namespace Preference.Editor.Project
{
    public static class Hover
    {
        public static void OnGUI(string guid, Rect selectionRect)
        {
            if (Event.current.type == EventType.Repaint)
            {
                selectionRect.width += selectionRect.x;
                selectionRect.x = 0;

                if (selectionRect.Contains(Event.current.mousePosition))
                {
                    var color = new Color(
                        EditorGUIUtility.isProSkin ? 1f : 0f,
                        EditorGUIUtility.isProSkin ? 1f : 0f,
                        EditorGUIUtility.isProSkin ? 1f : 0f,
                        0.06f);

                    EditorGUI.DrawRect(selectionRect, color);
                }

                if (EditorWindow.mouseOverWindow != null && Event.current.type == EventType.MouseMove)
                {
                    EditorWindow.mouseOverWindow.Repaint();
                    Event.current.Use();
                }
            }
        }
    }
}