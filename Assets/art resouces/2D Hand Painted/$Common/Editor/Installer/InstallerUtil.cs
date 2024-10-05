using System.IO;
using UnityEditor;

namespace NotSlot.HandPainted2D.Editor
{
  internal static class InstallerUtil
  {
    #region Methods

    public static void DeleteInstallerMenu ()
    {
      if ( !File.Exists(Installer.Config.INSTALL_MENU_SCRIPT) )
        return;

#if !HANDPAINTED2D_SANDBOX
      AssetDatabase.DeleteAsset(Installer.Config.INSTALL_MENU_SCRIPT);
      AssetDatabase.Refresh();
#endif
    }

    #endregion
  }
}