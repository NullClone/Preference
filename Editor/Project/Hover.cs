using UnityEditor;
using UnityEngine;

namespace Preference.Editor.Project
{
    public static class Hover
    {
        // Fields

        private static double LastCacheUpdateTime = -3d;

        private static EditorWindow ProjectWindow = null;


        // Methods

        public static void OnGUI(string guid, Rect selectionRect)
        {
            EditorApplication.update += () =>
            {
                if (ProjectWindow == null && EditorApplication.timeSinceStartup > LastCacheUpdateTime + 2d)
                {
                    LastCacheUpdateTime = EditorApplication.timeSinceStartup;

                    foreach (var window in Resources.FindObjectsOfTypeAll<EditorWindow>())
                    {
                        if (window.GetType().Name == "ProjectBrowser")
                        {
                            window.wantsMouseMove = true;

                            ProjectWindow = window;

                            break;
                        }
                    }
                }
            };

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

            if (Event.current.type == EventType.MouseMove)
            {
                EditorWindow.mouseOverWindow?.Repaint();

                Event.current.Use();
            }
        }
    }
}