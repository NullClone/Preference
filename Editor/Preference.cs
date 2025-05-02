using UnityEditor;

namespace Preference.Editor
{
    public static class Preference
    {
        // Fields

        public static bool Flag = true;

        public const string MenuPath = "Tools/Preference/Enable";


        // Methods

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            // Hierarchy

            EditorApplication.hierarchyWindowItemOnGUI += Hierarchy.Striping.OnGUI;
            EditorApplication.hierarchyWindowItemOnGUI -= Hierarchy.Striping.OnGUI;

            EditorApplication.hierarchyWindowItemOnGUI -= Hierarchy.Line.OnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += Hierarchy.Line.OnGUI;

            EditorApplication.hierarchyWindowItemOnGUI -= Hierarchy.Toggle.OnGUI;
            EditorApplication.hierarchyWindowItemOnGUI += Hierarchy.Toggle.OnGUI;


            // ProjectWindow

            EditorApplication.projectWindowItemOnGUI -= Project.Hover.OnGUI;
            EditorApplication.projectWindowItemOnGUI += Project.Hover.OnGUI;

            EditorApplication.projectWindowItemOnGUI -= Project.Striping.OnGUI;
            EditorApplication.projectWindowItemOnGUI += Project.Striping.OnGUI;

            EditorApplication.projectWindowItemOnGUI -= Project.Line.OnGUI;
            EditorApplication.projectWindowItemOnGUI += Project.Line.OnGUI;


            var value = EditorUserSettings.GetConfigValue(MenuPath);

            if (string.IsNullOrEmpty(value)) return;

            Flag = string.Equals(value, true.ToString(), System.StringComparison.OrdinalIgnoreCase);
        }

        [MenuItem(MenuPath)]
        private static void IsEnable()
        {
            Flag = !Flag;

            Menu.SetChecked(MenuPath, Flag);

            EditorUserSettings.SetConfigValue(MenuPath, Flag.ToString());
        }

        [MenuItem(MenuPath, validate = true)]
        private static bool IsEnableValidator()
        {
            Menu.SetChecked(MenuPath, Flag);

            return true;
        }
    }
}