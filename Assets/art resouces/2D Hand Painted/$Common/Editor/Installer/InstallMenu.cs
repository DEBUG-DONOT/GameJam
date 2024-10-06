using UnityEditor;

namespace NotSlot.HandPainted2D.Editor
{
  internal static class InstallMenu
  {
    #region Methods

    [MenuItem("2D Hand Painted/Install")]
    private static void ShowInstaller ()
    {
      Installer.ShowInstaller();
    }

    #endregion
  }
}