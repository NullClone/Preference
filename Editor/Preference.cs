using UnityEditor;

namespace Preference.Editor
{
    public static class Preference
    {
        [InitializeOnLoadMethod]
        static void Initialize() => Execute();

        static void Execute()
        {
            EditorApplication.hierarchyWindowItemOnGUI += Hierarchy.Toggle.OnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += Hierarchy.Striping.OnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += Hierarchy.Line.OnGUI;

            EditorApplication.projectWindowItemOnGUI += Project.Striping.OnGUI;
        }
    }
}