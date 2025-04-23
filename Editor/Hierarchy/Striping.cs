using UnityEditor;
using UnityEngine;

namespace Preference.Editor.Hierarchy
{
    public sealed class Striping
    {
        // Properties

        public static Color Color1 { get; set; } = new Color(
            EditorGUIUtility.isProSkin ? 1f : 0f,
            EditorGUIUtility.isProSkin ? 1f : 0f,
            EditorGUIUtility.isProSkin ? 1f : 0f,
            EditorGUIUtility.isProSkin ? 0.033f : 0.05f);

        public static Color Color2 { get; set; } = new Color(0f, 0f, 0f, 0f);


        // Methods

        public static void OnGUI(int instanceID, Rect selectionRect)
        {
            if (Event.current.type != EventType.Repaint) return;

            selectionRect.xMin = 32;

            if ((int)(selectionRect.y - 4) / 16 % 2 == 1)
            {
                EditorGUI.DrawRect(selectionRect, Color1);
            }
            else
            {
                EditorGUI.DrawRect(selectionRect, Color2);
            }
        }
    }
}